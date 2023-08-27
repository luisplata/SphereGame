namespace ServiceLocatorPath
{
    public interface ISoundSfxService : IServices
    {
        void PlaySound(string nameOfSfx);
        void PlaySound(string nameOfSfx, bool isRepeating);
    }
}