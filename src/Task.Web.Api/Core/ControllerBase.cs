﻿using System;
using System.Security;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Task.Common;

namespace Task.Core
{
    public class ControllerBase : Controller
    {
        protected IActionResult GetHttpResponse(Func<IActionResult> codeToExecute)
        {
            IActionResult response;

            try
            {
                response = codeToExecute.Invoke();
            }
            catch (SecurityException)
            {
                response = new HttpUnauthorizedResult();
            }
            catch (TaskNotFoundException ex)
            {
                response = new HttpNotFoundObjectResult(ex.Message);
            }
            catch (Exception)
            {
                response = new HttpStatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}