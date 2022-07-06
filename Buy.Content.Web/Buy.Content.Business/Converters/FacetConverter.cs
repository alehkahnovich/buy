using Buy.Content.Contract.Search.Facets;
using System.Linq;
using Buy.Content.Contract.Search.Facets.Extensions;
using Buy.Content.Search.Core.Types;
using BL = Buy.Content.Business.Representation;

namespace Buy.Content.Business.Converters
{
    public static class FacetConverter
    {
        public static BL.Search.FacetText Convert(FacetText facet) {
            var text = SetBase<BL.Search.FacetText>(facet);
            text.Value = facet.Value;
            return text;
        }

        public static BL.Search.FacetDate Convert(FacetDate facet) {
            var text = SetBase<BL.Search.FacetDate>(facet);
            text.Value = facet.Value;
            return text;
        }

        public static BL.Search.FacetNumber Convert(FacetNumber facet) {
            var text = SetBase<BL.Search.FacetNumber>(facet);
            text.Value = facet.Value;
            return text;
        }

        public static BL.Search.FacetRangeDate Convert(FacetRangeDate facet) {
            var text = SetBase<BL.Search.FacetRangeDate>(facet);
            text.FromValue = facet.FromValue;
            text.ToValue = facet.ToValue;
            return text;
        }

        public static BL.Search.FacetRangeNumber Convert(FacetRangeNumber facet) {
            var text = SetBase<BL.Search.FacetRangeNumber>(facet);
            text.FromValue = facet.FromValue;
            text.ToValue = facet.ToValue;
            return text;
        }

        private static TFacet SetBase<TFacet>(Facet contract) where TFacet : BL.Search.Facet, new() {
             var facet = new TFacet {
                Id = contract.Id,
                Facets = contract.Facets?.Select(entry => (TFacet)Convert((dynamic)entry))
            };
            return facet;
        }

        //contracts

        public static FacetText Convert(BL.Search.FacetText facet) {
            var text = SetBase<FacetText>(facet);
            text.Value = facet.Value;
            return text;
        }

        public static FacetDate Convert(BL.Search.FacetDate facet) {
            var text = SetBase<FacetDate>(facet);
            text.Value = facet.Value;
            return text;
        }

        public static FacetNumber Convert(BL.Search.FacetNumber facet) {
            var text = SetBase<FacetNumber>(facet);
            text.Value = facet.Value;
            return text;
        }


        public static FacetRangeDate Convert(BL.Search.FacetRangeDate facet) {
            var text = SetBase<FacetRangeDate>(facet);
            text.FromValue = facet.FromValue;
            text.ToValue = facet.ToValue;
            return text;
        }

        public static FacetRangeNumber Convert(BL.Search.FacetRangeNumber facet) {
            var text = SetBase<FacetRangeNumber>(facet);
            text.FromValue = facet.FromValue;
            text.ToValue = facet.ToValue;
            return text;
        }

        private static TFacet SetBase<TFacet>(BL.Search.Facet contract) where TFacet : Facet, new() {
            var facet = new TFacet {
                Id = contract.Id,
                Type = contract.Type,
                Count = contract.Count,
                Facets = contract.Facets?.Select(entry => (TFacet)Convert((dynamic)entry))
            };
            return facet;
        }

        //search contracts

        public static FacetText Convert(Search.Core.Contracts.Facets.Text contract) {
            var text = SetBase<FacetText>(contract);
            text.Value = contract.Value.ToArray();
            text.Type = FacetType.Text.GetEnumDescription();
            return text;
        }

        public static FacetDate Convert(Search.Core.Contracts.Facets.Date contract) {
            var text = SetBase<FacetDate>(contract);
            text.Value = contract.Value.ToArray();
            text.Type = FacetType.Date.GetEnumDescription();
            return text;
        }

        public static FacetNumber Convert(Search.Core.Contracts.Facets.Number contract) {
            var text = SetBase<FacetNumber>(contract);
            text.Value = contract.Value.ToArray();
            text.Type = FacetType.Number.GetEnumDescription();
            return text;
        }

        private static TFacet SetBase<TFacet>(Search.Core.Contracts.Facets.Property contract) where TFacet : Facet, new() {
            var facet = new TFacet {
                Id = contract.Id,
            };
            return facet;
        }
    }
}