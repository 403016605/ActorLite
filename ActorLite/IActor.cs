namespace ActorLite
{
    internal interface IActor
    {
        bool Exited { get; }

        int MessageCount { get; }

        ActorContext Context { get; }
        void Execute();
    }
}