using System;
using System.Collections.Generic;
using System.Text;

namespace WebProjectOOP.Business
{
    public interface IAiService
    {
        Task<string> GenerateDescriptionAsync(string title);
    }
}
