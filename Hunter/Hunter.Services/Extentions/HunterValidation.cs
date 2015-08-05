using System.ComponentModel.DataAnnotations;
using Hunter.Services.Services.Interfaces;

namespace Hunter.Services.Extentions
{
    public class HunterValidation
    {
        private static IPoolService _poolService;

        public HunterValidation(IPoolService poolService)
        {
            _poolService = poolService;
        }

        public static ValidationResult ValidateIsPoolNameExist(string name)
        {
            var isValid = _poolService.IsPoolNameExist(name);

            return isValid ? new ValidationResult("Pool name alreafy exists!") : ValidationResult.Success;
        }
    }
}
