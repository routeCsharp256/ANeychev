using CSharpCourse.Core.Lib.Enums;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class MerchPackType : Enumeration
    {
        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при устройстве на работу.
        /// </summary>
        public static MerchPackType WelcomePack = new((int) MerchType.WelcomePack, nameof(WelcomePack));

        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при посещении конференции в качестве слушателя.
        /// </summary>
        public static MerchPackType ConferenceListenerPack =
            new((int) MerchType.ConferenceListenerPack, nameof(ConferenceListenerPack));

        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера.
        /// </summary>
        public static MerchPackType ConferenceSpeakerPack =
            new((int) MerchType.ConferenceSpeakerPack, nameof(ConferenceSpeakerPack));

        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при успешном прохождении испытательного срока.
        /// </summary>
        public static MerchPackType ProbationPeriodEndingPack =
            new((int) MerchType.ProbationPeriodEndingPack, nameof(ProbationPeriodEndingPack));

        /// <summary>
        /// Набор мерча, выдаваемый сотруднику за выслугу лет.
        /// </summary>
        public static MerchPackType VeteranPack = new((int) MerchType.VeteranPack, nameof(VeteranPack));

        public MerchPackType(int id, string name) : base(id, name)
        {
        }
    }
}