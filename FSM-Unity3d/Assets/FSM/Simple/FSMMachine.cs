using System.Collections;
using System.Collections.Generic;

namespace CLToolKits.FSM.Simple
{
	public class FSMMachine  
	{
		bool running = false;
		List<Status> statuses = new List<Status>();
		public Status DefaultStatus{get;private set;}
		public Status CurrentStatus{get;private set;}

		//构造函数中添加默认状态
		public FSMMachine(Status defaultStatus)
		{
			DefaultStatus = defaultStatus;
			AddStatus(defaultStatus);
		}

		//添加状态
		public void AddStatus(Status status)
		{
			if(statuses.Contains(status))
			return;

			statuses.Add(status);
		}

		//移除状态
		public void RemoveStatus(Status status)
		{
			if(!statuses.Contains(status))
			return;

			statuses.Remove(status);
		}

		public void Run()
		{
			running = true;
			CurrentStatus = DefaultStatus;
			CurrentStatus.OnEnter();
		}

		//改变状态
		public void ChangeStatus(Status status)
		{
			if(!statuses.Contains(status)||!!running)
			return;
			if(CurrentStatus!=null)
				CurrentStatus.OnLeave();

			status.OnEnter();
			CurrentStatus = status;
		}
	}
}
