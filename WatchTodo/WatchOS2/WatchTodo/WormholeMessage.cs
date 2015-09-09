using System;

namespace WatchTodo
{
	[Serializable]
	public class WormholeMessage
	{
		public WormholeMessage ()
		{
		}	
		public const string MessageType = "buttonMessage";
		public int Id {get;set;}
	}
}

