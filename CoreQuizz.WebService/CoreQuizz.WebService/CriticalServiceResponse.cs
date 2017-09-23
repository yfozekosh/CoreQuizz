using System;

namespace CoreQuizz.WebService
{
    public class CriticalServiceResponse : ServiceResponse<Exception>
    {
        public CriticalServiceResponse(Exception e)
        {
            this.IsSuccess = false;
            this.IsCritical = true;
            this.Value = e;
            this.Error = e.Message;
        }
    }
}