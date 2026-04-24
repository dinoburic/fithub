using FitHub.Domain.Common;

namespace FitHub.Domain.Entities.Catalog;

/// <summary>
/// Represents a product category.
/// </summary>
public class ProductCategoryEntity : BaseEntity
{
    /// <summary>
    /// Name of the category.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Indicates whether the category is active (enabled).
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// List of products that belong to this category.
    ///
    /// **Note for students:**
    /// This collection is used primarily for reading (querying),
    /// not for updates. We use <see cref="IReadOnlyCollection{T}"/>
    /// with a <c>private set</c> to prevent direct manipulation
    /// of the list content in code (e.g., <c>category.Products.Add(...)</c>).
    ///
    /// EF Core can still load products using <c>Include</c>,
    /// but it will not track changes in this collection during
    /// <c>SaveChanges</c>.
    ///
    /// Technically, you can use a regular <see cref="ICollection{T}"/>
    /// and add products through navigation, but that often introduces
    /// complications:
    /// - EF Core can lose entity state tracking,
    /// - it may attempt to create the relationship twice,
    /// - and it makes validation and business rules harder (e.g., blocking
    ///   product additions to a disabled category).
    ///
    /// That is why this class uses a read-only approach: the category knows
    /// which products are associated with it, while updates are performed only
    /// through <see cref="ProductEntity"/> which contains <c>CategoryId</c>.
    /// </summary>
    public IReadOnlyCollection<ProductEntity> Products { get; private set; } = new List<ProductEntity>();

    /// <summary>
    /// Single source of truth for technical/business constraints.
    /// Used in validators and EF configuration.
    /// </summary>
    public static class Constraints
    {
        public const int NameMaxLength = 100;
    }
}
