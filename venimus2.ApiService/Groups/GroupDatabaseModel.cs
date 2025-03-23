using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace venimus2.ApiService.Groups;

[Table("group")]
public class Group
{
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    ///     The unique external code for the group.  For example YorkCodeDojo
    /// </summary>
    [Column("slug")]
    public string? Slug { get; set; }

    /// <summary>
    ///     Is this group still actively running events?
    /// </summary>
    [Column("is_active")]
    public bool IsActive { get; set; }

    /// <summary>
    /// The unique name for the group / community.  For example York Code Dojo
    /// </summary>
    [Column("name")]
    public string? Name { get; set; }

    /// <summary>
    /// A description of the group in markdown
    /// </summary>
    [Column("description")]
    public string? Description { get; set; }

    /// <summary>
    /// A very short one-line description of the group
    /// </summary>
    [Column("strap_line")]
    public string? StrapLine { get; set; }

    /// <summary>
    ///     The name of this groups Slack channel
    /// </summary>
    [Column("slack_channel_name")]
    public string? SlackChannelName { get; set; }

    /// <summary>
    ///     The group's logo.
    /// </summary>
    [Column("logo_base64")]
    public string? LogoInBase64 { get; set; }
}