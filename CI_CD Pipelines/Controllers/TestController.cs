using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CI_CD_Pipelines.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("fixed")]
public class TestController : ControllerBase
{

    [HttpGet("int")]
    public ActionResult<int> IntFunc()
    {
        return Ok(13);
    }

    [HttpGet("string")]
    [DisableRateLimiting]
    public ActionResult<string> StringFunc()
    {
        return Ok("Ziad");
    }


    [HttpPost("string")]
    public ActionResult<string> StringPost([FromBody] string a )
    {
        return Ok(a);
    }

    

}