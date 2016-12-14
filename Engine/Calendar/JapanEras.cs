// <copyright file="JapanEras.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;

namespace Engine.Chrono
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// http://www.japan-guide.com/e/e2126.html
    /// http://blogs.msdn.com/b/michkap/archive/2013/02/27/10397438.aspx
    /// http://en.wikipedia.org/wiki/List_of_Japanese_era_names
    /// http://blogs.msdn.com/b/shawnste/archive/2011/11/15/extending-the-windows-japanese-calendar-era-information.aspx
    /// http://blogs.msdn.com/b/shawnste/archive/2009/09/24/japanese-calendars-how-do-i-test-support-for-additional-eras.aspx
    /// [HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Nls\Calendars\Japanese\Eras]
    /// </remarks>
    public class JapanEras
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>http://www.sljfaq.org/afaq/era-list.html</remarks>
        public Dictionary<DateTime, string> erra = new Dictionary<DateTime, string>
        {
            //{new DateTime(498, 1, 1), "大化"}, // Taika?
            //{new DateTime(645, 01, 01), "大化"},  //  Taika
            //{new DateTime(650, 01, 01), "白雉"},  //  Hakuchi
            //{new DateTime(686, 01, 01), "朱鳥"},  //  Shuchō
            //{new DateTime(701, 01, 01), "大宝"},  //  Taihō
            //{new DateTime(704, 01, 01), "慶雲"},  //  Keiun
            //{new DateTime(708, 01, 01), "和銅"},  //  Wadō
            //{new DateTime(715, 01, 01), "霊亀"},  //  Reiki
            //{new DateTime(717, 01, 01), "養老"},  //  Yōrō
            //{new DateTime(724, 01, 01), "神亀"},  //  Jinki
            //{new DateTime(729, 01, 01), "天平"},  //  Tempyō
            //{new DateTime(749, 01, 01), "天平感宝"},  //  Tempyōkampō
            //{new DateTime(749, 01, 01), "天平勝宝"},  //  Tempyōshōhō
            //{new DateTime(757, 01, 01), "天平宝字"},  //  Tempyōjōji
            //{new DateTime(765, 01, 01), "天平神護"},  //  Tempyōjingo
            //{new DateTime(767, 01, 01), "神護景雲"},  //  Jingokeiun
            //{new DateTime(770, 01, 01), "宝亀"},  //  Hōki
            //{new DateTime(781, 01, 01), "天応"},  //  Ten'ō
            //{new DateTime(782, 01, 01), "延暦"},  //  Enryaku
            //{new DateTime(806, 01, 01), "大同"},  //  Daidō
            //{new DateTime(810, 01, 01), "弘仁"},  //  Kōnin
            //{new DateTime(823, 01, 01), "天長"},  //  Tenchō
            //{new DateTime(834, 01, 01), "承和"},  //  Jōwa
            //{new DateTime(848, 01, 01), "嘉祥"},  //  Kashō
            //{new DateTime(851, 01, 01), "仁寿"},  //  Ninju
            //{new DateTime(855, 01, 01), "斉衡"},  //  Saikō
            //{new DateTime(857, 01, 01), "天安"},  //  Ten'an
            //{new DateTime(859, 01, 01), "貞観"},  //  Jōgan
            //{new DateTime(877, 01, 01), "元慶"},  //  Gangyō
            //{new DateTime(885, 01, 01), "仁和"},  //  Ninna
            //{new DateTime(889, 01, 01), "寛平"},  //  Kampyō
            //{new DateTime(898, 01, 01), "昌泰"},  //  Shōtai
            //{new DateTime(901, 01, 01), "延喜"},  //  Engi
            //{new DateTime(923, 01, 01), "延長"},  //  Enchō
            //{new DateTime(931, 01, 01), "承平"},  //  Jōhei
            //{new DateTime(938, 01, 01), "天慶"},  //  Tengyō
            //{new DateTime(947, 01, 01), "天暦"},  //  Tenryaku
            //{new DateTime(957, 01, 01), "天徳"},  //  Tentoku
            //{new DateTime(961, 01, 01), "応和"},  //  Ōwa
            //{new DateTime(964, 01, 01), "康保"},  //  Kōhō
            //{new DateTime(968, 01, 01), "安和"},  //  Anna
            //{new DateTime(970, 01, 01), "天禄"},  //  Tenroku
            //{new DateTime(974, 01, 01), "天延"},  //  Ten'en
            //{new DateTime(976, 01, 01), "貞元"},  //  Jōgen
            //{new DateTime(979, 01, 01), "天元"},  //  Tengen
            //{new DateTime(983, 01, 01), "永観"},  //  Eikan
            //{new DateTime(985, 01, 01), "寛和"},  //  Kanna
            //{new DateTime(987, 01, 01), "永延"},  //  Eien
            //{new DateTime(989, 01, 01), "永祚"},  //  Eiso
            //{new DateTime(990, 01, 01), "正暦"},  //  Shōryaku
            //{new DateTime(995, 01, 01), "長徳"},  //  Chōtoku
            //{new DateTime(999, 01, 01), "長保"},  //  Chōhō
            //{new DateTime(1004, 01, 01), "寛弘"},  //  Kankō
            //{new DateTime(1013, 01, 01), "長和"},  //  Chōwa
            //{new DateTime(1017, 01, 01), "寛仁"},  //  Kannin
            //{new DateTime(1021, 01, 01), "治安"},  //  Jian
            //{new DateTime(1024, 01, 01), "万寿"},  //  Manju
            //{new DateTime(1028, 01, 01), "長元"},  //  Chōgen
            //{new DateTime(1037, 01, 01), "長暦"},  //  Chōryaku
            //{new DateTime(1040, 01, 01), "長久"},  //  Chōkyū
            //{new DateTime(1045, 01, 01), "寛徳"},  //  Kantoku
            //{new DateTime(1046, 01, 01), "永承"},  //  Eishō
            //{new DateTime(1053, 01, 01), "天喜"},  //  Tengi
            //{new DateTime(1058, 01, 01), "康平"},  //  Kōhei
            //{new DateTime(1065, 01, 01), "治暦"},  //  Jiryaku
            //{new DateTime(1069, 01, 01), "延久"},  //  Enkyū
            //{new DateTime(1074, 01, 01), "承保"},  //  Jōhō
            //{new DateTime(1078, 01, 01), "承暦"},  //  Jōryaku
            //{new DateTime(1081, 01, 01), "永保"},  //  Eihō
            //{new DateTime(1084, 01, 01), "応徳"},  //  Ōtoku
            //{new DateTime(1087, 01, 01), "寛治"},  //  Kanji
            //{new DateTime(1095, 01, 01), "嘉保"},  //  Kahō
            //{new DateTime(1097, 01, 01), "永長"},  //  Eichō
            //{new DateTime(1098, 01, 01), "承徳"},  //  Jōtoku
            //{new DateTime(1099, 01, 01), "康和"},  //  Kōwa
            //{new DateTime(1104, 01, 01), "長治"},  //  Chōji
            //{new DateTime(1106, 01, 01), "嘉承"},  //  Kajō
            //{new DateTime(1108, 01, 01), "天仁"},  //  Tennin
            //{new DateTime(1110, 01, 01), "天永"},  //  Tennei
            //{new DateTime(1113, 01, 01), "永久"},  //  Eikyū
            //{new DateTime(1118, 01, 01), "元永"},  //  Gen'ei
            //{new DateTime(1120, 01, 01), "保安"},  //  Hōan
            //{new DateTime(1124, 01, 01), "天治"},  //  Tenji
            //{new DateTime(1126, 01, 01), "大治"},  //  Daiji
            //{new DateTime(1131, 01, 01), "天承"},  //  Tenshō
            //{new DateTime(1132, 01, 01), "長承"},  //  Chōshō
            //{new DateTime(1135, 01, 01), "保延"},  //  Hōen
            //{new DateTime(1141, 01, 01), "永治"},  //  Eiji
            //{new DateTime(1142, 01, 01), "康治"},  //  Kōji
            //{new DateTime(1144, 01, 01), "天養"},  //  Ten'yō
            //{new DateTime(1145, 01, 01), "久安"},  //  Kyūan
            //{new DateTime(1151, 01, 01), "仁平"},  //  Nimpei
            //{new DateTime(1154, 01, 01), "久寿"},  //  Kyūju
            //{new DateTime(1156, 01, 01), "保元"},  //  Hōgen
            //{new DateTime(1159, 01, 01), "平治"},  //  Heiji
            //{new DateTime(1160, 01, 01), "永暦"},  //  Eiryaku
            //{new DateTime(1161, 01, 01), "応保"},  //  Ōhō
            //{new DateTime(1163, 01, 01), "長寛"},  //  Chōkan
            //{new DateTime(1165, 01, 01), "永万"},  //  Eiman
            //{new DateTime(1166, 01, 01), "仁安"},  //  Nin'an
            //{new DateTime(1169, 01, 01), "嘉応"},  //  Kaō
            //{new DateTime(1171, 01, 01), "承安"},  //  Shōan
            //{new DateTime(1175, 01, 01), "安元"},  //  Angen
            //{new DateTime(1177, 01, 01), "治承"},  //  Jishō
            //{new DateTime(1181, 01, 01), "養和"},  //  Yōwa
            //{new DateTime(1182, 01, 01), "寿永"},  //  Juei
            //{new DateTime(1184, 01, 01), "元暦"},  //  Genryaku
            //{new DateTime(1185, 01, 01), "文治"},  //  Bunji
            //{new DateTime(1190, 01, 01), "建久"},  //  Kenkyū
            //{new DateTime(1199, 01, 01), "正治"},  //  Shōji
            //{new DateTime(1201, 01, 01), "建仁"},  //  Kennin
            //{new DateTime(1204, 01, 01), "元久"},  //  Genkyū
            //{new DateTime(1206, 01, 01), "建永"},  //  Ken'ei
            //{new DateTime(1207, 01, 01), "承元"},  //  Jōgen
            //{new DateTime(1211, 01, 01), "建暦"},  //  Kenryaku
            //{new DateTime(1214, 01, 01), "建保"},  //  Kempō
            //{new DateTime(1219, 01, 01), "承久"},  //  Jōkyū
            //{new DateTime(1222, 01, 01), "貞応"},  //  Jōō
            //{new DateTime(1225, 01, 01), "元仁"},  //  Gennin
            //{new DateTime(1225, 01, 01), "嘉禄"},  //  Karoku
            //{new DateTime(1228, 01, 01), "安貞"},  //  Antei
            //{new DateTime(1229, 01, 01), "寛喜"},  //  Kanki
            //{new DateTime(1232, 01, 01), "貞永"},  //  Jōei
            //{new DateTime(1233, 01, 01), "天福"},  //  Tempuku
            //{new DateTime(1235, 01, 01), "文暦"},  //  Bunryaku
            //{new DateTime(1235, 01, 01), "嘉禎"},  //  Katei
            //{new DateTime(1239, 01, 01), "暦仁"},  //  Ryakunin
            //{new DateTime(1239, 01, 01), "延応"},  //  En'ō
            //{new DateTime(1240, 01, 01), "仁治"},  //  Ninji
            //{new DateTime(1243, 01, 01), "寛元"},  //  Kangen
            //{new DateTime(1247, 01, 01), "宝治"},  //  Hōji
            //{new DateTime(1249, 01, 01), "建長"},  //  Kenchō
            //{new DateTime(1256, 01, 01), "康元"},  //  Kōgen
            //{new DateTime(1257, 01, 01), "正嘉"},  //  Shōka
            //{new DateTime(1259, 01, 01), "正元"},  //  Shōgen
            //{new DateTime(1260, 01, 01), "文応"},  //  Bun'ō
            //{new DateTime(1261, 01, 01), "弘長"},  //  Kōchō
            //{new DateTime(1264, 01, 01), "文永"},  //  Bun'ei
            //{new DateTime(1275, 01, 01), "健治"},  //  Kenji
            //{new DateTime(1278, 01, 01), "弘安"},  //  Kōan
            //{new DateTime(1288, 01, 01), "正応"},  //  Shōō
            //{new DateTime(1293, 01, 01), "永仁"},  //  Einin
            //{new DateTime(1299, 01, 01), "正安"},  //  Shōan
            //{new DateTime(1303, 01, 01), "乾元"},  //  Kengen
            //{new DateTime(1303, 01, 01), "嘉元"},  //  Kagen
            //{new DateTime(1307, 01, 01), "徳治"},  //  Tokuji
            //{new DateTime(1308, 01, 01), "延慶"},  //  Enkyō
            //{new DateTime(1311, 01, 01), "応長"},  //  Ōchō
            //{new DateTime(1312, 01, 01), "正和"},  //  Shōwa
            //{new DateTime(1317, 01, 01), "文保"},  //  Bumpō
            //{new DateTime(1319, 01, 01), "元応"},  //  Gen'ō
            //{new DateTime(1321, 01, 01), "元亨"},  //  Genkō
            //{new DateTime(1325, 01, 01), "正中"},  //  Shōchu
            //{new DateTime(1326, 01, 01), "嘉暦"},  //  Karyaku
            //{new DateTime(1329, 01, 01), "元徳"},  //  Gentoku
            //{new DateTime(1331, 01, 01), "元弘"},  //  Genkō (Southern)
            //{new DateTime(1332, 01, 01), "正慶"},  //  Shōkei
            //{new DateTime(1334, 01, 01), "建武"},  //  Kemmu (Southern)
            //{new DateTime(1336, 01, 01), "延元"},  //  Eigen (Southern)
            //{new DateTime(1338, 01, 01), "暦応"},  //  Ryakuō
            //{new DateTime(1340, 01, 01), "興国"},  //  Kōkoku (Southern)
            //{new DateTime(1342, 01, 01), "康永"},  //  Kōei
            //{new DateTime(1345, 01, 01), "貞和"},  //  Jōwa
            //{new DateTime(1347, 01, 01), "正平"},  //  Shōhei (Southern)
            //{new DateTime(1350, 01, 01), "観応"},  //  Kan'ō
            //{new DateTime(1352, 01, 01), "文和"},  //  Bunna
            //{new DateTime(1356, 01, 01), "延文"},  //  Embun
            //{new DateTime(1361, 01, 01), "康安"},  //  Kōan
            //{new DateTime(1362, 01, 01), "貞治"},  //  Jōji
            //{new DateTime(1368, 01, 01), "応安"},  //  Ōan
            //{new DateTime(1370, 01, 01), "建徳"},  //  Kentoku (Southern)
            //{new DateTime(1372, 01, 01), "文中"},  //  Bunchū (Southern)
            //{new DateTime(1375, 01, 01), "永和"},  //  Eiwa
            //{new DateTime(1375, 01, 01), "天授"},  //  Tenju (Southern)
            //{new DateTime(1379, 01, 01), "康暦"},  //  Kōryaku
            //{new DateTime(1381, 01, 01), "永徳"},  //  Eitoku
            //{new DateTime(1381, 01, 01), "弘和"},  //  Kōwa (Southern)
            //{new DateTime(1384, 01, 01), "至徳"},  //  Shitoku
            //{new DateTime(1384, 01, 01), "元中"},  //  Genchū (Southern)
            //{new DateTime(1387, 01, 01), "嘉慶"},  //  Kakei
            //{new DateTime(1389, 01, 01), "康応"},  // 康 Kōō
            //{new DateTime(1390, 01, 01), "明徳"},  // 明 Meitoku
            //{new DateTime(1394, 01, 01), "応永"},  // 応 Ōei
            //{new DateTime(1428, 01, 01), "正長"},  // 正 Shōchō
            //{new DateTime(1429, 01, 01), "永享"},  // 永 Eikyō
            //{new DateTime(1441, 01, 01), "嘉吉"},  // 嘉 Kakitsu
            //{new DateTime(1444, 01, 01), "文安"},  // 文 Bun'an
            //{new DateTime(1449, 01, 01), "宝徳"},  // 宝 Hōtoku
            //{new DateTime(1452, 01, 01), "享徳"},  // 享 Kyōtoku
            //{new DateTime(1455, 01, 01), "康正"},  // 康 Kōshō
            //{new DateTime(1457, 01, 01), "長禄"},  // 長 Chōroku
            //{new DateTime(1461, 01, 01), "寛正"},  // 寛 Kanshō
            //{new DateTime(1466, 01, 01), "文正"},  // 文 Bunshō
            //{new DateTime(1467, 01, 01), "応仁"},  // 応 Ōnin
            //{new DateTime(1469, 01, 01), "文明"},  // 文 Bummei
            //{new DateTime(1487, 01, 01), "長享"},  // 長 Chōkyō
            //{new DateTime(1489, 01, 01), "延徳"},  // 延 Entoku
            //{new DateTime(1492, 01, 01), "明応"},  // 明 Meiō
            //{new DateTime(1501, 01, 01), "文亀"},  // 文 Bunki
            //{new DateTime(1504, 01, 01), "永正"},  // 永 Eishō
            //{new DateTime(1521, 01, 01), "大永"},  // 大 Daiei
            //{new DateTime(1528, 01, 01), "享禄"},  // 享 Kyōroku
            //{new DateTime(1532, 01, 01), "天文"},  // 天 Tembun
            //{new DateTime(1555, 01, 01), "弘治"},  // 弘 Kōji
            //{new DateTime(1558, 01, 01), "永禄"},  // 永 Eiroku
            //{new DateTime(1570, 01, 01), "元亀"},  // 元 Genki
            //{new DateTime(1573, 01, 01), "天正"},  // 天 Tenshō
            //{new DateTime(1593, 01, 01), "文禄"},  // 文 Bunroku
            //{new DateTime(1596, 01, 01), "慶長"},  // 慶 Keichō
            //{new DateTime(1615, 01, 01), "元和"},  // 元 Genna
            //{new DateTime(1624, 01, 01), "寛永"},  // 寛 Kan'ei
            //{new DateTime(1645, 01, 01), "正保"},  // 正 Shōhō
            //{new DateTime(1648, 01, 01), "慶安"},  // 慶 Keian
            //{new DateTime(1652, 01, 01), "承応"},  // 承 Jōō
            //{new DateTime(1655, 01, 01), "明暦"},  // 明 Meireki
            //{new DateTime(1658, 01, 01), "万治"},  // 万 Manji
            //{new DateTime(1661, 01, 01), "寛文"},  // 寛 Kambun
            //{new DateTime(1673, 01, 01), "延宝"},  // 延 Empō
            //{new DateTime(1681, 01, 01), "天和"},  // 天 Tenna
            //{new DateTime(1684, 01, 01), "貞享"},  // 貞 Jōkyō
            //{new DateTime(1688, 01, 01), "元禄"},  // 元 Genroku
            //{new DateTime(1704, 01, 01), "宝永"},  // 宝 Hōei
            //{new DateTime(1711, 01, 01), "正徳"},  // 正 Shōtoku
            //{new DateTime(1716, 01, 01), "享保"},  // 享 Kyōhō
            //{new DateTime(1736, 01, 01), "元文"},  // 元 Gembun
            //{new DateTime(1741, 01, 01), "寛保"},  // 寛 Kampō
            //{new DateTime(1744, 01, 01), "延享"},  // 延 Enkyō
            //{new DateTime(1748, 01, 01), "寛延"},  // 寛 Kan'en
            //{new DateTime(1751, 01, 01), "宝暦"},  // 宝 Hōreki
            //{new DateTime(1764, 01, 01), "明和"},  // 明 Meiwa
            //{new DateTime(1773, 01, 01), "安永"},  // 安 An'ei
            //{new DateTime(1781, 01, 01), "天明"},  // 天 Temmei
            //{new DateTime(1801, 01, 01), "寛政"},  // 寛 Kansei
            //{new DateTime(1802, 01, 01), "享和"},  // 享 Kyōwa
            //{new DateTime(1804, 01, 01), "文化"},  // 文 Bunka
            //{new DateTime(1818, 01, 01), "文政"},  // 文 Bunsei
            //{new DateTime(1831, 01, 01), "天保"},  // 天 Tempō
            //{new DateTime(1845, 01, 01), "弘化"},  // 弘 Kōka
            //{new DateTime(1848, 01, 01), "嘉永"},  // 嘉 Kaei
            //{new DateTime(1855, 01, 01), "安政"},  // 安 Ansei
            //{new DateTime(1860, 01, 01), "万延"},  // 万 Man'ei
            //{new DateTime(1861, 01, 01), "文久"},  // 文 Bunkyū
            //{new DateTime(1864, 01, 01), "元治"},  // 元 Genji

            {new DateTime(1865, 01, 01), "慶応"},  // 慶 Keiō
            {new DateTime(1868, 01, 01), "明治"}, // 明 Meiji
            {new DateTime(1912, 07, 30), "大正"}, // 大 Taishō
            {new DateTime(1926, 12, 25), "昭和"}, // 昭 Shōwa
            {new DateTime(1989, 01, 08), "平成"} // 平 Heisei
        };
    }
}
