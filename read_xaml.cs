Window window = null;
using (FileStream fs = new FileStream("MyWindow.xaml"), FileMode.Open, FileAccess.Read){
	window = (Window)XamlReader.Load(fs);
}

StackPanel panel = (StackPanel)window.Content;
Button okButton = (Button)panel.Children[4]


OpenFileDialog
