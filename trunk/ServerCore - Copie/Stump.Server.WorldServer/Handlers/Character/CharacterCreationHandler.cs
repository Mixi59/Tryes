
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Stump.Core.Attributes;
using Stump.DofusProtocol.Messages.Framework.IO;
using Stump.Database.WorldServer;
using Stump.Database.WorldServer.Character;
using Stump.DofusProtocol.Classes;
using Stump.DofusProtocol.Classes.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Breeds;
using Stump.Server.WorldServer.Entities;
using Stump.Server.WorldServer.Global;
using Stump.Server.WorldServer.Manager;
using Stump.Server.WorldServer.Threshold;

namespace Stump.Server.WorldServer.Handlers
{
    public partial class CharacterHandler : WorldHandlerContainer
    {
        [Variable]
        public static bool EnableNameSuggestion = true;

        /// <summary>
        ///   Maximum number of characters you can create/store in your account
        /// </summary>
        [Variable]
        public static uint MaxCharacterSlot = 5;

        [WorldHandler(typeof(CharacterCreationRequestMessage))]
        public static void HandleCharacterCreationRequestMessage(WorldClient client, CharacterCreationRequestMessage message)
        {
            /* Check if we can create characters on this server */
            if (client.Characters.Count >= MaxCharacterSlot)
            {
                client.Send(new CharacterCreationResultMessage((int)CharacterCreationResultEnum.ERR_TOO_MANY_CHARACTERS));
                return;
            }

            /* Check if name is free */
            if (CharacterRecord.IsNameExists(message.name))
            {
                client.Send(new CharacterCreationResultMessage((int)CharacterCreationResultEnum.ERR_NAME_ALREADY_EXISTS));
                return;
            }

            string characterName = StringExtensions.FirstLetterUpper(message.name.ToLower());

            /* Check is name is well formatted */
            if (!Regex.IsMatch(characterName, "^[A-Z][a-z]{2,9}(?:-[A-Z][a-z]{2,9}|[a-z]{1,10})$", RegexOptions.Compiled))
            {
                client.Send(new CharacterCreationResultMessage((int)CharacterCreationResultEnum.ERR_INVALID_NAME));
                return;
            }

            /* Get character Breed */
            BaseBreed breed = BreedManager.GetBreed(message.breed);

            /* Check if breed is available */
            if (!client.Account.CanUseBreed(message.breed) || !BreedManager.AvailableBreeds.Contains(breed.Id))
            {
                client.Send(new CharacterCreationResultMessage((int)CharacterCreationResultEnum.ERR_NOT_ALLOWED));
                return;
            }

            /* Parse character colors */
            var indexedColors = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                int color = message.colors[i];

                if (color == -1)
                    color = (int)(!message.sex ? breed.MaleColors[i] : breed.FemaleColors[i]);

                indexedColors.Add(int.Parse((i + 1) + color.ToString("X6"), NumberStyles.HexNumber));
            }

            var breedLook = !message.sex ? breed.MaleLook.Copy() : breed.FemaleLook.Copy();
            breedLook.indexedColors = indexedColors;

            /* Create Inventory */
            // TODO ADD START OBJECTS
            var inventory = new InventoryRecord { Kamas = (uint)breed.StartKamas };
            inventory.Create();

            /* Create Character */
            var character = new CharacterRecord
            {
                Experience = ThresholdManager.Thresholds["CharacterExp"].GetLowerBound((uint)breed.StartLevel),
                Name = characterName,
                Breed = message.breed,
                Sex = message.sex ? SexTypeEnum.SEX_FEMALE : SexTypeEnum.SEX_MALE,
                BaseLook = breedLook,
                MapId = (int)breed.StartMap,
                CellId = breed.StartCellId,
                BaseHealth = breed.StartHealthPoint,
                DamageTaken = 0,
                StatsPoints = 0,
                SpellsPoints = 0,
                Strength = 0,
                Vitality = 0,
                Wisdom = 0,
                Intelligence = 0,
                Chance = 0,
                Agility = 0,
                Inventory = inventory
            };
            character.Create();

            /* Set Character SpellCollection */
            foreach (SpellIdEnum spellId in breed.StartSpells.Keys)
            {
                var spell = new SpellRecord { SpellId = (uint)spellId, Position = breed.StartSpells[spellId], Level = 1 };
                spell.Create();
                character.Spells.Add(spell);
            }

            /* Save it */
            CharacterManager.CreateCharacterOnAccount(character, client);

            BasicHandler.SendBasicNoOperationMessage(client);
            client.Send(new CharacterCreationResultMessage((int)CharacterCreationResultEnum.OK));
            SendCharactersListMessage(client);
        }

        [WorldHandler(typeof(CharacterNameSuggestionRequestMessage))]
        public static void HandleCharacterNameSuggestionRequestMessage(WorldClient client, CharacterNameSuggestionRequestMessage message)
        {
            if (!EnableNameSuggestion)
            {
                client.Send(new CharacterNameSuggestionFailureMessage((uint)NicknameGeneratingFailureEnum.NICKNAME_GENERATOR_UNAVAILABLE));
                return;
            }
            string generatedName = CharacterManager.GenerateName();

            client.Send(new CharacterNameSuggestionSuccessMessage(generatedName));
        }
    }
}