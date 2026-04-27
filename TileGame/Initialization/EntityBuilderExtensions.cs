using EcsLib.Common.Components;
using EcsLib.Drawing.Components;
using EcsLib.Drawing.Enums;
using EcsLib.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileGame.Initialization;

public static class EntityBuilderExtensions
{
    public static EntityBuilder.InnerBuilder WithTransform(this EntityBuilder.InnerBuilder builder,
        Vector2 position, Vector2? scale = null, float rotation = 0f) => builder
            .With(new Position(position))
            .With(new Scale(scale ?? Vector2.One))
            .With(new Rotation(rotation));

    public static EntityBuilder.InnerBuilder WithParent(this EntityBuilder.InnerBuilder builder,
        int parentEntityId) => builder
            .With(new ParentEntity(EntityBuilder.GetPacked(parentEntityId)));

    public static EntityBuilder.InnerBuilder WithParentDeltaTransform(this EntityBuilder.InnerBuilder builder,
        Vector2? position = null, Vector2? scale = null, float? rotation = null)
    {
        if (position.HasValue)
            builder.With(new DeltaPosition(position.Value));

        if (scale.HasValue)
            builder.With(new DeltaScale(scale.Value));

        if (rotation.HasValue)
            builder.With(new DeltaRotation(rotation.Value));

        return builder;
    }

    public static EntityBuilder.InnerBuilder WithSprite(this EntityBuilder.InnerBuilder builder,
        Texture2D texture) => builder
            .With(new Sprite(texture))
            .With(new FillSpriteRequest());

    public static EntityBuilder.InnerBuilder WithTileSprite(this EntityBuilder.InnerBuilder builder,
        Texture2D texture, Point tileCoordinate) => builder
            .With(new Sprite(texture))
            .With(new AtlasTile(tileCoordinate, Game1.TileSize))
            .With(new FillSpriteRequest());

    public static EntityBuilder.InnerBuilder WithAlignment(this EntityBuilder.InnerBuilder builder,
        Alignment horizontal = Alignment.Center, Alignment vertical = Alignment.Center) => builder
            .With(new SpriteAlignment(horizontal, vertical));

    public static EntityBuilder.InnerBuilder WithAlignment(this EntityBuilder.InnerBuilder builder,
        Alignment bothAlignments = Alignment.Center) => builder
            .With(new SpriteAlignment(bothAlignments, bothAlignments));
}
