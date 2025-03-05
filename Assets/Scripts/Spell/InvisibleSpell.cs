public class InvisibleSpell : GenericSpell
{
    protected override void SpellEffect()
    {
        PlayerEffectsControl.Instance.ApplyInvisibleEffect();
    }
}
