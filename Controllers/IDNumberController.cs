using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDNoValidator.Data;
using IDNoValidator.Models;
using System.Net;
using System.Net.Http;


namespace IDNoValidator.Controllers
{
  [Route("api/IDNoValidator")]
  [ApiController]
  public class IDNumberController : Controller
  {
    private readonly ValidatorDbContext _context;

    public IDNumberController(ValidatorDbContext context)
    {
      _context = context;
    }

    [Route("ValidateIDNumber")]
    [HttpPost]
    public IActionResult ValidateIDNumber([FromBody] IDNumberModel model)
    {
      string idNumber = model.IDNumber;
      bool isValid;
      string details = "";
      var existingId = _context.IDNumbers.FirstOrDefault(entry => entry.IDNumber == idNumber);

      if (existingId != null)
      {
        isValid = existingId.IsValid;
        details = existingId.Details;
      }
      else
      {
        if (idNumber.Length == 13)
        {

          int idYear = int.Parse(idNumber.Substring(0, 2));

          if (idYear <= 99)
          {
            int idMonth = int.Parse(idNumber.Substring(2, 2));

            if (idMonth <= 12)
            {
              int idDate = int.Parse(idNumber.Substring(4, 2));

              if (idDate <= 31)
              {
                int idCitizenship = int.Parse(idNumber.Substring(10, 1));

                if (idCitizenship == 0 || idCitizenship == 1)
                {
                  int digit12 = int.Parse(idNumber.Substring(11, 1));

                  if (digit12 == 8)
                  {
                    int lastDigit = int.Parse(idNumber.Substring(12, 1));

                    int sumOdds = 0;
                    string newNumberStr = "";

                    for (int i = 0; i < idNumber.Length - 1; i += 2)
                    {
                      if (char.IsDigit(idNumber[i]))
                      {
                        int digit = int.Parse(idNumber[i].ToString());
                        sumOdds += digit;
                      }
                    }

                    for (int j = 1; j < idNumber.Length; j += 2)
                    {
                      if (char.IsDigit(idNumber[j]))
                      {
                        newNumberStr += idNumber[j];
                      }
                    }

                    int newNumber = int.Parse(newNumberStr);
                    int newNumber2 = newNumber * 2;
                    string newNumber2Str = newNumber2.ToString();
                    int sum2 = 0;

                    for (int k = 0; k < newNumber2Str.Length; k++)
                    {
                      if (char.IsDigit(newNumber2Str[k]))
                      {
                        int digit = int.Parse(newNumber2Str[k].ToString());
                        sum2 += digit;
                      }
                    }

                    int totalSum = sum2 + sumOdds;

                    string totalSumStr = totalSum.ToString();

                    string totalSumSecondDigit = totalSumStr.Substring(1, 1);

                    int calculatedLastDigit = int.Parse(totalSumSecondDigit);

                    if (lastDigit == calculatedLastDigit)
                    {
                      isValid = true;
                    }
                    else
                    {
                      isValid = false;
                      details += " Last Digit Checksum Failed; ";
                    }
                  }
                  else
                  {
                    isValid = false;
                    details += " 12th Digit should be 8; ";
                  }
                }
                else
                {
                  isValid = false;
                  details += " Invalid Citizenship status; ";
                }
              }
              else
              {
                isValid = false;
                details += " Invalid day of birth; ";
              }
            }
            else
            {
              isValid = false;
              details += "Invalid month of birth; ";
            }
          }
          else
          {
            isValid = false;
            details += " Invalid year of birth; ";
          }
        }
        else
        {
          isValid = false;
          details = " ID Number should be 13 digits; ";
        }
      }

      model.IsValid = isValid;
      model.Details = details;      

      _context.IDNumbers.Add(model);
      _context.SaveChanges();
      return Ok(model);
    }
  }
}
