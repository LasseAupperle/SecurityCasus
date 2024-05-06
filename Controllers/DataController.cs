using Microsoft.AspNetCore.Mvc;
using SecurityCasus.Data;
using SecurityCasus.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityCasus;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase
{
    private readonly AppDbContext _context;

    public DataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MyData>>> GetData()
    {
        var dataList = await _context.Test.ToListAsync(); 
        var decryptedDataList = dataList.Select(d => new MyData
        {
            Id = d.Id,
            Test = /*EncryptionHelper.DecryptData*/(d.Test) 
        }).ToList();

        return Ok(decryptedDataList);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteData(int id)
    {
        var data = await _context.Test.FindAsync(id);
        if (data == null)
        {
            return NotFound();
        }

        _context.Test.Remove(data);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<MyData>> AddData([FromBody] MyData data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var encryptedData = /*EncryptionHelper.EncryptData*/(data.Test);
        var newData = new MyData { Test = encryptedData };

        _context.Test.Add(newData);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetData), new { id = newData.Id }, newData);
    }
}

