﻿// /*************************************************************************
//  *
//  *  Copyright (C) 2010 - 2011 Stump Team
//  *
//  *  This program is free software: you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation, either version 3 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  *
//  *************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.BaseCore.Framework.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Data;
using Stump.Server.BaseServer.Initializing;
using BreedData = Stump.DofusProtocol.D2oClasses.Breed;

namespace Stump.Server.WorldServer.Breeds
{
    public static class BreedManager
    {
        /// <summary>
        /// List of available breeds
        /// </summary>
        [Variable]
        public static List<PlayableBreedEnum> AvailableBreeds = new List<PlayableBreedEnum>
            {
                PlayableBreedEnum.Feca,
                PlayableBreedEnum.Osamodas,
                PlayableBreedEnum.Enutrof,
                PlayableBreedEnum.Sram,
                PlayableBreedEnum.Xelor,
                PlayableBreedEnum.Ecaflip,
                PlayableBreedEnum.Eniripsa,
                PlayableBreedEnum.Iop,
                PlayableBreedEnum.Cra,
                PlayableBreedEnum.Sadida,
                PlayableBreedEnum.Sacrieur,
                PlayableBreedEnum.Pandawa,
                PlayableBreedEnum.Roublard,
                //BreedEnum.Zobal,
            };

        /// <summary>
        ///   Array containing all breeds.
        /// </summary>
        private static readonly BaseBreed[] BaseBreeds = new BaseBreed[AvailableBreeds.Count + 1]; // there is no breed at index 0

        /// <summary>
        ///   List containing every data for each breed.
        /// </summary>
        private static BreedData[] m_breedsData;

        /// <summary>
        ///   Load breeds data from database.
        ///   Called once on World Initialization process.
        /// </summary>
        [StageStep(Stages.Two, "Loaded Breeds")]
        public static void LoadBreedsData()
        {
            m_breedsData = DataLoader.LoadData<BreedData>().ToArray();

            InitBreed(new FecaBreed());
            InitBreed(new EcaflipBreed());
            InitBreed(new EniripsaBreed());
            InitBreed(new EnutrofBreed());
            InitBreed(new OsamodasBreed());
            InitBreed(new SramBreed());
            InitBreed(new XelorBreed());
            InitBreed(new CraBreed());
            InitBreed(new IopBreed());
            InitBreed(new PandawaBreed());
            InitBreed(new SacrieurBreed());
            InitBreed(new SadidaBreed());
            InitBreed(new RoublardBreed());
            //InitBreed(new ZobalBreed());
        }

        /// <summary>
        ///   Initialize a breed with initial data.
        /// </summary>
        /// <param name = "breed">breed to initialize</param>
        private static void InitBreed(BaseBreed breed)
        {
            var breedData = m_breedsData.Single(entry => (PlayableBreedEnum)entry.id == breed.Id);
            breed.Initialize(breedData);
            BaseBreeds[(int) breed.Id] = breed;
        }

        #region BreedEnum Getter

        public static BaseBreed GetBreed(PlayableBreedEnum breed)
        {
            return BaseBreeds[(int) breed];
        }

        public static BaseBreed GetBreed(int breed)
        {
            return BaseBreeds[breed];
        }

        public static uint BreedsToFlag(IEnumerable<PlayableBreedEnum> breeds)
        {
            return (uint)breeds.Aggregate(0, (current, breedEnum) => current | (1 << (int)breedEnum));
        }

        #endregion
    }
}