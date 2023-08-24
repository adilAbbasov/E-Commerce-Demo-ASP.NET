namespace ECommerce.Helpers
{
	public static class UploadFileHelper
	{
		public async static Task<string> UploadFile(IFormFile file)
		{
			var fs = new FileStream(@$"wwwroot/images/admin/{file.FileName}", FileMode.Create);
			await file.CopyToAsync(fs);
			
			return fs.Name;
		}
	}
}
