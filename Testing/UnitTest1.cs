using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        class Model : ViewModelBase.ViewModelBase
        {
            public string Prop1
            {
                get { return base.GetValue(() => this.Prop1); }
                set { base.SetValue(() => this.Prop1, value); }
            }
        }


        [TestMethod]
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
            }).Wait(new TimeSpan(0, 0, 30));

            Assert.IsTrue(changeDetected);
        }
    }
}
