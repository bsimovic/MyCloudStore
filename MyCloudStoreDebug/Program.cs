using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCloudStoreLib;

namespace MyCloudStoreDebug
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(MyCloudStoreLib.MD5.Hash(Encoding.ASCII.GetBytes("The quick brown fox jumps over the lazy dog.")));
			Console.ReadLine();
		}
	}
}
