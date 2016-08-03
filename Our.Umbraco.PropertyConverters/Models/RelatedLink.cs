namespace Our.Umbraco.PropertyConverters.Models
{
	public class RelatedLink
	{
		public string Caption { get; internal set; }
		public bool NewWindow { get; internal set; }
		public bool IsInternal { get; internal set; }
		public RelatedLinkType Type { get; internal set; }
		public string Link { get; internal set; }
		public int? Id { get; internal set; }
		internal bool IsDeleted { get; set; }

	}
}
