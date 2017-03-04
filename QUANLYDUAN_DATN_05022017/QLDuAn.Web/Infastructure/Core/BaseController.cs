using QLDuAn.Model.Models;
using QLDuAn.Service;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace QLDuAn.Web.Infastructure.Core
{
    public class BaseController : ApiController
    {
        private ErrorService _errorService;

        public BaseController(ErrorService errorService)
        {
            this._errorService = errorService;
        }

        public HttpResponseMessage CreateReponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage messageRespose = null;
            try
            {
                messageRespose = function.Invoke();
            }
            catch (DbEntityValidationException dbVEx)
            {
                foreach (var item in dbVEx.EntityValidationErrors)
                {
                    Trace.WriteLine($"entity of type \"{item.Entry.Entity.GetType().Name} \" in state\"{item.Entry.State}\" has the following validation errors");

                    foreach (var ex in item.ValidationErrors)
                    {
                        Trace.WriteLine($"-Property \"{ex.PropertyName} \" Error \"{ex.ErrorMessage}\"");
                    }
                }
                this.LogError(dbVEx);

                // su ly ben trong len phải view inner
                messageRespose = request.CreateResponse(HttpStatusCode.BadRequest, dbVEx.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                this.LogError(dbEx);
                // su ly ben trong len phải view inner
                messageRespose = request.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                this.LogError(ex);

                // trả về lỗi 404
                messageRespose = request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return messageRespose;
        }

        private void LogError(Exception errorException)
        {
            try
            {
                Errors error = new Errors();
                error.Message = errorException.Message;
                error.Stacktrace = errorException.StackTrace;
                error.Created_at = DateTime.Now;
                _errorService.Add(error);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}