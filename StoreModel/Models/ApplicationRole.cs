
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StoreModel.Models
{
    public class ApplicationRole: IdentityRole
    {

        public ApplicationRole()
        {

        }

        public ApplicationRole(string name) : base(name)
        {

        }

    }
}