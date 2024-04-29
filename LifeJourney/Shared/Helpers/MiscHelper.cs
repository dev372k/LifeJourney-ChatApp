using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public class MiscHelper
    {
        public static string GetInitials(string input)
        {
            string[] words = input.Split(' ');
            string initials = "";

            foreach (string word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    initials += word[0];
                }
            }

            return initials;
        }
    }
}
