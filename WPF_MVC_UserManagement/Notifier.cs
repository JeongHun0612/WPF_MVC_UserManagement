using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace WPF_MVC_UserManagement
{
    /// <summary>
    /// PropertyChanged 이벤트를 thread-safe하게 호출하는 클래스
    /// 호출 스레드가 Dispacher와 연결된 스레드이면 직접 호출하고, 그렇지 않고 다른 스레드이면
    /// Dispacher를 통한 호출을 한다.
    /// </summary>
    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 이 오브젝트와 연결된 Dispacher를 설정하거나 반환한다.
        /// </summary>
        public Dispatcher Dispatcher { get; private set; }

        /// <summary>
        /// Application의 Dispacher를 기본으로하여 생성된다.
        /// </summary>
        public Notifier()
        {
            Dispatcher = Application.Current.Dispatcher;
        }

        /// <summary>
        /// 주어진 dispatcher로 생성된다.
        /// </summary>
        /// <param name="dispatcher"></param>
        public Notifier(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        delegate void PropertyChangedDelegate(object obj, PropertyChangedEventArgs args);

        /// <summary>
        /// PropertyChanged event를 호출한다.
        /// </summary>
        /// <param name="name">변경된 Property 이름</param>
        protected void Notify(string name)
        {
            if (PropertyChanged != null)
            {
                if (Dispatcher.CheckAccess())
                {
                    // 호출 스레드가 Dispacher와 연결된 스레드이면 직접 호출한다.
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
                else
                {
                    // 호출 스레드가 Dispacher와 연결된 스레드가 아니면 Dispatcher를 통해 호출한다.
                    Dispatcher.Invoke(new PropertyChangedDelegate(PropertyChanged), this, new PropertyChangedEventArgs(name));
                }
            }
        }
    }

    public class NotifierCollection : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        delegate void PropertyChangedDelegate(object obj, PropertyChangedEventArgs args);

        /// <summary>
        /// PropertyChanged event를 호출한다.
        /// </summary>
        /// <param name="name">변경된 Property 이름</param>
        protected void NotifyCollection(string name)
        {
            if (PropertyChanged != null)
            {
                if (Application.Current.Dispatcher.CheckAccess())
                {
                    // 호출 스레드가 Dispacher와 연결된 스레드이면 직접 호출한다.
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
                else
                {
                    // 호출 스레드가 Dispacher와 연결된 스레드가 아니면 Dispatcher를 통해 호출한다.
                    Application.Current.Dispatcher.Invoke(new PropertyChangedDelegate(PropertyChanged), this, new PropertyChangedEventArgs(name));
                }
            }
        }
    }
}
