using NUnit.Framework;
using System;
namespace Tests
{
	[TestFixture()]
	public class Test
	{

		class Model : ViewModelBase.ViewModelBase
		{
			public string Prop1
			{
				get { return base.GetValue(() => this.Prop1); }
				set { base.SetValue(() => this.Prop1, value); }
			}
		}

		[Test()]
		public void TestPropertyChangedNotification()
		{
			var model = new Model();

			bool changeDetected = false;

			model.PropertyChanged += (s, args) =>
			{
				if (string.Equals(args.PropertyName, "Prop1"))
				{
					changeDetected = true;
				}
			};

			System.Threading.Tasks.Task.Run(() =>
			{
				// need to introduce a small delay into this so that property change can fire
				model.Prop1 = "Hello World";
			}).Wait(new TimeSpan(0,0,30));

			Assert.IsTrue(changeDetected);
		}
	}
}
