﻿using System.Collections;
using Discord.Commands;

namespace shigLeBot
{
    internal class Server
    {
        private ulong id;
        private List<Message> messages = new List<Message>();
        private List<Command> commands = new List<Command>();
        private List<IEnumerator> jobs = new List<IEnumerator>();

        public Server(ulong id)
        {
            this.id = id;

            Program.servers.Add(id, this);
            Program.serverloops.Add(ServerLoop());
        }

        public void AddMessage(CommandContext context)
        {
            lock (messages)
            {
                messages.Add(new Message(context));
            }
        }

        public IEnumerator ServerLoop()
        {
            while (true)
            {
                // たまっているメッセージを変換できる場合はjobに変換する
                lock (messages)
                {
                    foreach (Message message in messages)
                    {
                        // commands内から適しているcommandを探す
                        foreach (var command in commands)
                        {
                            // messageがcommandに適しているか調べる
                            if (command.command == message.context.Message.Content)
                            {
                                jobs.Add(command.NewJob(message));
                                break;
                            }

                            yield return null;
                        }
                        yield return null;
                    }
                    messages.Clear();
                }

                yield return null;

                // たまっているジョブを順に処理していく
                for (int i = jobs.Count - 1; i >= 0; i--)
                {
                    if (!jobs[i].MoveNext())
                    {
                        // もしjobが終了したならjobsから消す
                        jobs.RemoveAt(i);
                    }
                }

                yield return null;
            }
        }
    }
}
