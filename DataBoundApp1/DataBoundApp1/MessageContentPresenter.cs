using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataBoundApp1
{
    public class MessageContentPresenter : ContentControl
    {
        /// <summary>
        /// The DataTemplate to use when Message.Side == Side.Me
        /// </summary>
        public DataTemplate MeTemplate { get; set; }

        /// <summary>
        /// The DataTemplate to use when Message.Side == Side.You
        /// </summary>
        public DataTemplate YouTemplate { get; set; }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            // apply the required template
            Message message = newContent as Message;
            if (message.Side == MessageSide.Me)
            {
                ContentTemplate = MeTemplate;
            }
            else
            {
                ContentTemplate = YouTemplate;
            }
        }
    }
}
