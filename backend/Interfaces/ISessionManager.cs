namespace backend.Interfaces
{
    using backend.Models;

    public interface ISessionManager
    {
        string CreateSession(UserSession session);
        bool TryGetSession(string sessionId, out UserSession session);
        void RemoveSession(string sessionId);
        void CleanupExpiredSessions();
    }
}