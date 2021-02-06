using System.Collections.Generic;

namespace AdOut.Point.Model.Classes
{
    public class ValidationResult<TError>
    {
        public bool IsSuccessed
        {
            get
            {
                return Errors?.Count == 0;
            }
        }

        public List<TError> Errors { get; set; }

        public ValidationResult()
        {
            Errors = new List<TError>();
        }
    }
}
