using MScResearchTool.Server.Core.Models;
using System;
using System.Collections.Generic;

namespace MScResearchTool.Server.Web.Builders
{
    public class CrackingBuilder
    {
        public int NumberOfDistributions { get; set; }
        public string Password { get; set; }
        public string FileName { get; set; }
        public string SerializedArchiveContent { get; set; }
        public byte[] ArchiveContent { get; set; }
        public IList<char> CharactersDictionary { get; set; }

        public Cracking Build()
        {
            var crackingBuilt = new Cracking()
            {
                CreationDate = DateTime.Now,
                IsFinished = false,
                IsAvailable = true,
                DroidRanges = NumberOfDistributions,
                ArchivePassword = Password,
                FileName = FileName,
                SerializedContentFound = SerializedArchiveContent,
                ArchiveToCrack = ArchiveContent,
                AvailableCharacters = CharactersDictionary
            };

            return crackingBuilt;
        }
    }
}
