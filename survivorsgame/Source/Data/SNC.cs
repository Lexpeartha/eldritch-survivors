using Godot;

namespace SurvivorsGame.Source.Data;

public static class StringNameCache
{
  public readonly struct ItemTags
  {
    // Melee
    public static readonly StringName Melee = "Melee";
    // Firearm
    public static readonly StringName Firearm = "Firearm";
    public static readonly StringName HitScan = "HitScan";
    public static readonly StringName Projectile = "Projectile";
    // Magic
    public static readonly StringName Magic = "Magic";
    public static readonly StringName Fire = "Fire";
    public static readonly StringName Poison = "Poison";
    public static readonly StringName Ice = "Ice";
    public static readonly StringName Psychic = "Psychic";
    public static readonly StringName Electric = "Electric";
    // Equipment
    public static readonly StringName Equipment = "Equipment";
    public static readonly StringName Lightweight = "Lightweight";
    public static readonly StringName Bulky = "Bulky";
    // Other
    public static readonly StringName Book = "Book";
    public static readonly StringName Staff = "Staff";
    public static readonly StringName Throwable = "Throwable"; // ??
    public static readonly StringName Consumable = "Consumable"; // ??
  }
  
  public readonly struct Traits
  {
    public static readonly StringName OnPreDamagePhase = "OnPreDamagePhase";
    public static readonly StringName OnDamagePhase = "OnDamagePhase";
    public static readonly StringName OnPostDamagePhase = "OnPostDamagePhase";
  }
}