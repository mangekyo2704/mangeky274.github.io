using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FruitShopSolution.ViewModel.Validation
{
    public class ValidateFileAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as List<IFormFile>;
            if (file.Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
