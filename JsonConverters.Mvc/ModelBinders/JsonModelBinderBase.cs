using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EntyTea.JsonConverters.Mvc.ModelBinders
{
    /// <summary>
    /// An abstract implementation of <c>IModelBinder</c> that parses a JSON string.
    /// </summary>
    public abstract class JsonModelBinderBase : IModelBinder
    {
        public virtual object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var contentType = controllerContext.HttpContext.Request.ContentType;
            if (!contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            using (var requestInputStream = controllerContext.HttpContext.Request.InputStream)
            {
                requestInputStream.Seek(0, SeekOrigin.Begin);
                using (var requestInputReader = new StreamReader(requestInputStream))
                {
                    return BindModel(requestInputReader);
                }
            }
        }

        /// <summary>
        /// Binds the model to a value by using the specified JSON string.
        /// </summary>
        /// <param name="json">a reader of the JSON data to parse</param>
        /// <returns>the bound value or null if no value was bound</returns>
        protected abstract object BindModel(TextReader json);
    }

}
