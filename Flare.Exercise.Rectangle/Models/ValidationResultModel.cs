using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flare.Exercise.Rectangle.Models
{
    public class ValidationResultModel
    {

        private readonly bool _isSuccessful;
        private readonly string _errorMessage;

        public ValidationResultModel(bool isSuccessful, string errorMessage)
        {
            _isSuccessful = isSuccessful;
            _errorMessage = errorMessage;
        }

        public bool IsSuccesful { get { return _isSuccessful; }  }

        public string ErrorMessage { get { return _errorMessage; } }
    }
}
