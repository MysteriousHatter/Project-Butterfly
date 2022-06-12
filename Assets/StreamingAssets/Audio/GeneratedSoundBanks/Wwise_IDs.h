/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID LAPCLEARED = 1550399012U;
        static const AkUniqueID MENUSELECTBACK = 3107940081U;
        static const AkUniqueID MENUSELECTCHOOSE = 1040688795U;
        static const AkUniqueID MENUSELECTHIGHLIGHT = 88822120U;
        static const AkUniqueID PLAY_MUSICTHEME = 2332797262U;
        static const AkUniqueID PLAY_PLAYER_WINGFLAPS = 2113457283U;
        static const AkUniqueID PLAY_TESTBEEP = 1955951874U;
        static const AkUniqueID PLAY_TESTBEEP_3D = 1681067324U;
        static const AkUniqueID PLAY_TESTBEEP_LOOP = 523232931U;
        static const AkUniqueID PLAY_TESTBEEP_LOOP_3D = 3475882615U;
        static const AkUniqueID PLAYERHURT = 3537581393U;
        static const AkUniqueID PLAYERPICKUP = 2734591854U;
        static const AkUniqueID PLAYERVORTEX = 2762437514U;
        static const AkUniqueID SPEEDBOOST = 612788291U;
        static const AkUniqueID STAGECLEARED = 1582988071U;
        static const AkUniqueID STATUEFREED = 3424670137U;
        static const AkUniqueID STOP_MUSICTHEME = 1688769292U;
        static const AkUniqueID STOP_TESTBEEP_LOOP = 2989390457U;
        static const AkUniqueID STOP_TESTBEEP_LOOP_3D = 304008745U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace AREASTATE
        {
            static const AkUniqueID GROUP = 2064552269U;

            namespace STATE
            {
                static const AkUniqueID BEACH = 4075332698U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID WOODLAND = 3738767749U;
            } // namespace STATE
        } // namespace AREASTATE

        namespace GAMESTATE
        {
            static const AkUniqueID GROUP = 4091656514U;

            namespace STATE
            {
                static const AkUniqueID INCREDITS = 3820324306U;
                static const AkUniqueID INGAME = 984691642U;
                static const AkUniqueID INMENU = 3374585465U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace GAMESTATE

        namespace PLAYERSTATE
        {
            static const AkUniqueID GROUP = 3285234865U;

            namespace STATE
            {
                static const AkUniqueID ALIVE = 655265632U;
                static const AkUniqueID DEAD = 2044049779U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace PLAYERSTATE

        namespace ROOMSTATE
        {
            static const AkUniqueID GROUP = 185713839U;

            namespace STATE
            {
                static const AkUniqueID INDOOR = 340398852U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID OUTDOOR = 144697359U;
            } // namespace STATE
        } // namespace ROOMSTATE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace GROUNDMATERIALSWITCH
        {
            static const AkUniqueID GROUP = 1044534455U;

            namespace SWITCH
            {
                static const AkUniqueID FOREST = 491961918U;
                static const AkUniqueID SAND = 803837735U;
                static const AkUniqueID STONE = 1216965916U;
            } // namespace SWITCH
        } // namespace GROUNDMATERIALSWITCH

        namespace PLAYERSPEEDSWITCH
        {
            static const AkUniqueID GROUP = 2051106367U;

            namespace SWITCH
            {
                static const AkUniqueID FLY = 1133470540U;
                static const AkUniqueID FLYBOOST = 940329477U;
                static const AkUniqueID RUN = 712161704U;
                static const AkUniqueID WALK = 2108779966U;
            } // namespace SWITCH
        } // namespace PLAYERSPEEDSWITCH

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID PLAYBACK_RATE = 1524500807U;
        static const AkUniqueID RPM = 796049864U;
        static const AkUniqueID RTPC_DISTANCE = 262290038U;
        static const AkUniqueID RTPC_PLAYERSPEED = 2653406601U;
        static const AkUniqueID SS_AIR_FEAR = 1351367891U;
        static const AkUniqueID SS_AIR_FREEFALL = 3002758120U;
        static const AkUniqueID SS_AIR_FURY = 1029930033U;
        static const AkUniqueID SS_AIR_MONTH = 2648548617U;
        static const AkUniqueID SS_AIR_PRESENCE = 3847924954U;
        static const AkUniqueID SS_AIR_RPM = 822163944U;
        static const AkUniqueID SS_AIR_SIZE = 3074696722U;
        static const AkUniqueID SS_AIR_STORM = 3715662592U;
        static const AkUniqueID SS_AIR_TIMEOFDAY = 3203397129U;
        static const AkUniqueID SS_AIR_TURBULENCE = 4160247818U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAINSOUNDBANK = 534561221U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIENTBEDS = 1182634443U;
        static const AkUniqueID AMBIENTMASTER = 1459460693U;
        static const AkUniqueID BOOST = 2389703494U;
        static const AkUniqueID ENDINGBUS = 3128577630U;
        static const AkUniqueID FLYINGMOTIONS = 2545167021U;
        static const AkUniqueID LAPCOUNTER = 1183712868U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MENU = 2607556080U;
        static const AkUniqueID MENUBUS = 4119570698U;
        static const AkUniqueID MENUSELECTION = 1516645226U;
        static const AkUniqueID NPCATTACK = 1448395294U;
        static const AkUniqueID NPCLOCOMOTION = 1217116541U;
        static const AkUniqueID NPCMASTER = 2033911932U;
        static const AkUniqueID NPCPATROL = 1214424580U;
        static const AkUniqueID NPCRETREAT = 3464610615U;
        static const AkUniqueID OVERWORLDBUS = 1689042417U;
        static const AkUniqueID PARALOOP = 2051369405U;
        static const AkUniqueID PAUSEMENU = 3494343696U;
        static const AkUniqueID PLAYERCOLLISION = 1457529962U;
        static const AkUniqueID PLAYERENEMYCOLLISION = 4081918166U;
        static const AkUniqueID PLAYERHAZARDCOLLISION = 2167524234U;
        static const AkUniqueID PLAYERLOCOMOTION = 2343802269U;
        static const AkUniqueID PLAYERMASTER = 3538689948U;
        static const AkUniqueID PLAYERPICKUPCOLLISION = 2699039134U;
        static const AkUniqueID SCENEUIMASTER = 1646606939U;
        static const AkUniqueID SCORE = 2398231425U;
        static const AkUniqueID SOUNDTRACKMASTER = 2408165297U;
        static const AkUniqueID SPAWNMASTER = 3613176568U;
        static const AkUniqueID TIMER = 3920142940U;
        static const AkUniqueID WINGFLAPS = 753391680U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID ANCIENTSTONERUINS = 3375396131U;
        static const AkUniqueID BEACH = 4075332698U;
        static const AkUniqueID DIGITAL = 889096775U;
        static const AkUniqueID GLITCH = 1380965534U;
        static const AkUniqueID OUTDOOR = 144697359U;
        static const AkUniqueID REVERBS = 3545700988U;
        static const AkUniqueID WOODLAND = 3738767749U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID DEFAULT_MOTION_DEVICE = 4230635974U;
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
