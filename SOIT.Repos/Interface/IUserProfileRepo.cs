﻿using SOIT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOIT.Repos.Interface
{
    public interface IUserProfileRepo
    {
        List<UserProfile> _GetAllUseProfile();
    }
}
