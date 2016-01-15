using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VSDA.Communication;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VSDA.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConnectionPage : Page
    {
        private IConnectionModuleViewModel module;

        public ConnectionPage(IConnectionModuleViewModel module)
        {
            this.module = module;
            this.DataContext = module;
            this.InitializeComponent();
            this.Loaded += this.PageLoaded;         
        }

        public async void PageLoaded(object sender, RoutedEventArgs e)
        {
            await this.module.InitializeModule();
            
            foreach(DeviceInformation device in this.module.Devices)
            {
                Button block = new Button()
                {
                    Content = device.Name,
                    FontSize = 18,
                    Height = 50,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    Command = new RelayCommand(delegate
                    {
                        this.module.CurrentDevice = device;
                    })

                };
                this.DevicesPanel.Children.Add(block);
            }
                       
            //this.InstructionsTextBlock.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis finibus erat elit, eget finibus purus egestas nec. Quisque justo nibh, imperdiet nec augue vitae, dictum ultrices justo. Donec velit dui, molestie vitae nibh ac, ultricies euismod leo. Phasellus laoreet tellus sem, sed interdum ex fermentum eget. Fusce nec pellentesque velit. Proin id enim ut metus egestas bibendum sed ac massa. Pellentesque pulvinar ac massa et vehicula. Mauris tincidunt tellus at nibh blandit, at porttitor eros porta. Nunc tempus iaculis tellus et venenatis.\n\nQuisque ultricies justo sit amet tempus dapibus. Duis a nisl vitae dolor iaculis scelerisque. Duis vulputate est nec feugiat malesuada. Mauris convallis tellus dui, ut pulvinar odio tempus et. Sed pretium lacinia lectus, ut consectetur felis varius sed. Mauris at quam accumsan, accumsan neque vitae, pretium odio. Sed elementum arcu sit amet felis gravida, in molestie enim vulputate. Praesent vel ligula ut risus semper ullamcorper placerat a quam. Proin id malesuada tellus. Donec tincidunt justo mi. Sed lacinia non magna sed ornare. Aenean tristique, elit sed congue rhoncus, orci magna fermentum nulla, ac tempus sapien est eget mi. Duis laoreet bibendum tortor. Sed malesuada mi libero, sodales euismod nulla aliquet sed.\n\nDonec sollicitudin, ligula id viverra pulvinar, risus nibh pharetra nunc, vel gravida nisl ligula at sem. Ut pharetra, augue eu sollicitudin blandit, magna est aliquet elit, at molestie odio ex eget ante. Nullam metus quam, cursus non rutrum sit amet, bibendum sed turpis. Proin aliquet imperdiet augue non aliquet. Suspendisse hendrerit justo in nisi efficitur finibus. Aliquam quis eros ac odio laoreet pharetra. Quisque consectetur lobortis justo, sed rutrum sapien sollicitudin et. Integer id libero at ligula semper mattis ac a sapien.\n\nVestibulum mattis, tellus non bibendum semper, augue dui condimentum est, ac rhoncus metus odio sit amet libero. Aliquam eget placerat mi, a imperdiet turpis. Nam ac risus at metus convallis pretium quis at orci. Vivamus hendrerit ultricies leo. Phasellus sodales malesuada nulla quis faucibus. Praesent vehicula quam in tristique faucibus. Sed a urna orci. Etiam sem magna, posuere eu vehicula et, venenatis pellentesque erat. Morbi at elit urna. Aliquam id orci accumsan, scelerisque ante a, interdum tortor. Duis non viverra augue. Aenean pretium interdum lorem, ac auctor purus tristique congue.\n\nPhasellus in augue venenatis, sollicitudin ex ac, sollicitudin leo. Ut volutpat, dui nec laoreet accumsan, tellus nunc viverra ex, sit amet tincidunt nisl ligula in massa. Fusce vitae odio a lectus auctor tristique eget eget lorem. Praesent sed mauris at est ultrices convallis. Donec eget molestie ipsum, vel maximus arcu. Aliquam malesuada arcu sit amet blandit pellentesque. Maecenas aliquet finibus dui, in viverra ex pellentesque ut. Curabitur elementum eros ut mi efficitur porta. Mauris eget porta ipsum. Duis id mi vulputate, dictum nulla vitae, molestie arcu. In aliquet fringilla nulla, id pretium elit eleifend a. Vivamus rutrum luctus velit quis pretium.\n\nEtiam vulputate viverra felis, ut pharetra est suscipit accumsan. Nam non ligula in mi consectetur auctor. Donec lobortis quam erat, ut euismod nisi hendrerit quis. Integer posuere dolor vitae lectus semper rutrum mattis et ex. Fusce egestas, eros sed tempus blandit, turpis est iaculis justo, eget condimentum nisi sem sit amet quam. Vestibulum ornare justo ut viverra pellentesque. Vivamus eu felis nec nisi vulputate viverra. Phasellus aliquet sollicitudin elementum. Nulla facilisi. Donec eu facilisis lectus. Nullam finibus volutpat risus vel vulputate. Ut nec diam nunc. Nullam malesuada, neque sed facilisis sodales, est nisl vestibulum odio, in facilisis lacus quam et massa.\n\nAliquam ut libero erat. Suspendisse egestas mauris ultricies odio fringilla aliquet. Donec placerat vitae elit ut molestie. Maecenas eget arcu pulvinar, consectetur mi id, scelerisque lectus. Sed laoreet magna eu lacus porttitor iaculis. Duis tincidunt at sem vitae ornare. Nunc malesuada, mauris eget porta fringilla, sem tortor commodo odio, et aliquam erat nulla at lectus. Aenean id laoreet odio.\n\nEtiam quis erat urna. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean sed interdum libero, sit amet dignissim mi. Aliquam cursus vulputate mattis. Integer molestie non ipsum vitae rhoncus. Vivamus consequat ullamcorper est. Aenean ullamcorper quis lacus in placerat. Donec eleifend sem sed dignissim viverra. Nulla porttitor dui in augue interdum ultrices. Donec nibh ligula, ultricies in tincidunt vitae, interdum molestie velit. Quisque dictum laoreet diam eu porttitor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Phasellus commodo suscipit libero, nec luctus massa lacinia sit amet. In in facilisis sapien, vitae faucibus nisi. Proin eleifend sed augue sit amet congue. Vestibulum eu vestibulum enim, vel sagittis sem.\n\nMaecenas ac auctor lectus. Aliquam venenatis elit eros, a tristique turpis ornare sit amet. Donec sagittis nunc vel auctor mattis. Quisque ornare lobortis lorem, et pellentesque urna ornare sed. Donec consequat lacinia turpis. Vivamus eu sollicitudin ante. Nam porttitor efficitur ornare. Nulla placerat mollis erat sit amet lobortis. Curabitur egestas neque dictum quam placerat tempor.\n\nProin vel tempus ligula. Aenean mattis ligula nulla, at auctor arcu ornare nec. Proin eget congue turpis. Donec tempor nibh sapien, congue venenatis risus porta vitae. Quisque nec metus vel augue vulputate tincidunt eget sed enim. Vivamus mauris ante, vestibulum a dictum et, accumsan tristique eros. Nullam consequat tincidunt odio ultrices auctor.\n\nNam pulvinar mi metus, scelerisque pharetra tellus accumsan ac. Aliquam sit amet elit vitae nisl vulputate mattis ac eget lectus. Donec non lacus ac ipsum molestie efficitur. Sed blandit pretium tellus ac sagittis. Morbi ut efficitur risus. Nulla sit amet convallis diam. Proin lorem velit, iaculis eget ipsum quis, finibus tempor turpis. Duis ac lectus sed metus viverra feugiat vel non magna. Aenean accumsan nibh a nibh ultrices, id ornare lacus pharetra. Nam sodales est quis magna euismod, laoreet posuere risus vehicula. In imperdiet velit sit amet nibh convallis, et fringilla neque sagittis. Proin posuere convallis dui sit amet lacinia. Nulla tristique convallis vulputate. Aliquam viverra posuere erat. Integer tempus, mi ut aliquet tincidunt, quam dolor tempus leo, eget dapibus ligula lorem quis turpis. Donec ultrices, risus vel tempus feugiat, urna metus suscipit augue, ac imperdiet tellus risus a eros. Maecenas justo justo, interdum eget ex a, sagittis malesuada felis. Quisque ac urna quis.";
        }
    }
}
