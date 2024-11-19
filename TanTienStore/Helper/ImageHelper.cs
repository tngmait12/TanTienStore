namespace TanTienStore.Helper
{
	public class ImageHelper
	{
		public static string UpLoadImage(IFormFile Img, string folder)
		{
			try
			{
				var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Img.FileName;
				var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", folder, fileName);

				using (var myFile = new FileStream(fullPath, FileMode.CreateNew))
				{
					Img.CopyTo(myFile);
				}
				return fileName;
			}
			catch (Exception ex) 
			{
				Console.WriteLine(ex.ToString());
				return string.Empty;
			}
		}
	}
}
