using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ourU_NetStandard
{
    public interface IAuthenticate
    {
        Task<bool> LoginAsync(bool useSilent = false);

        Task<bool> LogoutAsync();
    }
}
