using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Pusula.InternManagement.Exceptions;
using Pusula.InternManagement.Interns;
using Pusula.InternManagement.Permissions;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Users;

namespace Pusula.InternManagement.Files
{
    [Authorize(InternManagementPermissions.Files.Default)]
    public class FileAppService : ApplicationService, IFileAppService
    {
        private readonly IBlobContainer<FileContainer> _fileContainer;
        private readonly IFileRepository _fileRepository;
        private readonly IInternRepository _internRepository;
        private readonly ICurrentUser _currentUser;
        public FileAppService(
            IBlobContainer<FileContainer> fileContainer,
            IFileRepository fileRepository,
            IInternRepository internRepository,
            ICurrentUser currentUser)
        {
            _fileContainer = fileContainer;
            _fileRepository = fileRepository;
            _internRepository = internRepository;
            _currentUser = currentUser;
        }

        public async Task<PagedResultDto<FileDto>> GetListAsync(FileGetListInput input)
        {

            Log.Logger.Information($"File list requested with sorting: {input.Sorting}, skip count: {input.SkipCount}, max result count: {input.MaxResultCount}.");
            // Get a list of files from the repository, based on the given input parameters
            var files = await _fileRepository.GetListAsync(
               input.Sorting,
               input.SkipCount,
               input.MaxResultCount,
               _currentUser.GetId());

            // Get a queryable collection of interns from the repository
            var internQuery = await _internRepository.GetQueryableAsync();

            // Join the interns and files queries to create a new queryable object that includes the intern and file data
            var query = (from file in files
                         join intern in internQuery
                         on file.InternId equals intern.Id
                         select new { file, intern }).AsQueryable();

            // Execute the query and retrieve the results as a list
            var result = await AsyncExecuter.ToListAsync(query);

            // Map the query results to a list of FileDto objects
            var dtos = result.Select(x => new FileDto
            {
                Id = x.file.Id,
                Name = x.file.Name,
                InternId = x.intern.Id,
                InternName = $"{x.intern.Name} {x.intern.Surname}",
                CreationTime = x.file.CreationTime,
                CreatorId = x.file.CreatorId
            }).ToList();

            // Count the total number of intern in the repository, optionally filtering by name
            var totalCount = input.Filter == null
                ? await _fileRepository.CountAsync()
                : await _fileRepository.CountAsync(
                    file => file.Name.Contains(input.Filter));

            Log.Logger.Information($"Returning {files.Count} files with total count {totalCount}");

            // Return a PagedResultDto containing the total count of files and the list of FileDto objects
            return new PagedResultDto<FileDto>(
                totalCount,
                dtos
            );
        }

        public async Task<FileDto> GetFileAsync(GetFileRequestDto input)
        {
            // Get the byte array of the file with the given name for the given intern from the file repository
            Log.Logger.Information($"Getting file with name {input.Name} and intern id {input.InternId}");
            var file = await _fileContainer.GetAllBytesAsync($"{input.InternId}/{input.Name}");

            // Return the byte array as a FileDto object along with the file name
            return new FileDto
            {
                Name = input.Name,
                Content = file
            };
        }

        [Authorize(InternManagementPermissions.Files.Create)]
        public async Task SaveFileAsync(SaveFileInputDto input)
        {
            Log.Logger.Information($"Creating file");

            // Get the intern by id from the repository
            var intern = await _internRepository.GetAsync(input.InternId);

            // Construct the folder name where the file will be saved
            var blobFolderName = $"{intern.Id}/{input.Name}";

            // Check if the file extension is .pdf and save the file to the blob storage
            if (Path.GetExtension(input.Name) != ".pdf")
            {
                Log.Logger.Error($"The file extension is not supported. Please upload a pdf file.");
                throw new InvalidFileTypeException("pdf");
            }

            // Insert the new file entity into the repository
            await _fileRepository.InsertAsync(new File(GuidGenerator.Create(), input.InternId, input.Name));
            Log.Logger.Debug($"Successfully created a new file with name {input.Name} and intern ID {input.InternId}");

            // Save the file to the blob storage
            await _fileContainer.SaveAsync(blobFolderName, input.Content, true);
        }

        [Authorize(InternManagementPermissions.Files.Delete)]
        public async Task DeleteAsync(Guid internId, string fileName)
        {

            // Find the file in the repository by its name
            var fileFromRepository = await _fileRepository.FindByIdAndNameAsync(internId, fileName);

            // Delete the file from the repository
            await _fileRepository.DeleteAsync(fileFromRepository.Id);

            // Delete the file from the blob container
            await _fileContainer.DeleteAsync($"{internId}/{fileName}");
            Log.Logger.Debug($"File with ID {fileFromRepository.Id} has been deleted successfully");

        }
        public async Task<ListResultDto<InternLookupDto>> GetInternLookupAsync()
        {
            Log.Logger.Information("Retrieving list of interns");
            // Retrieve a list of interns asynchronously using the _internRepository's GetListAsync method
            var interns = await _internRepository.GetListAsync();

            // Return a ListResultDto containing the list of mapped InternLookupDto objects
            return new ListResultDto<InternLookupDto>(
                ObjectMapper.Map<List<Intern>, List<InternLookupDto>>(interns));
        }
    }
}
