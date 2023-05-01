using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Pusula.InternManagement.Pages;

public class Index_Tests : InternManagementWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
