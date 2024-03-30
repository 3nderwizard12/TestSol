using System;
namespace Model_Layer
{
	public class Result
	{
		public Result()
		{

		}

        public bool Correct { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public Exception? Ex { get; set; }

        public object? Object { get; set; }

        public List<object>? Objects { get; set; }
    }
}