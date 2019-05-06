﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TEAMModelOS.Admin.Controllers
{
	[Route("api/[controller]")]
	//[ApiController]
	public class TestController : Controller
	{
		[HttpGet("test")]
		public string Test(string test)
		{
			return "test"+test;
		}
	}
}
