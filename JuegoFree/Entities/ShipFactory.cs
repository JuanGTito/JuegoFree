using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JuegoFree.Core;
using JuegoFree.Properties;

namespace JuegoFree.Entities
{
    internal class ShipFactory
    {
        private static Point[] myNave1 = {
                new Point(24, 0), new Point(24, 0), new Point(24, 0), new Point(24, 0), new Point(24, 0),
                new Point(24, 0), new Point(23, 0), new Point(23, 0), new Point(23, 1), new Point(23, 1),
                new Point(23, 1), new Point(22, 1), new Point(22, 1), new Point(22, 1), new Point(22, 1),
                new Point(22, 1), new Point(22, 1), new Point(22, 1), new Point(22, 2), new Point(22, 2),
                new Point(22, 2), new Point(22, 2), new Point(22, 2), new Point(22, 2), new Point(22, 2),
                new Point(22, 2), new Point(22, 2), new Point(22, 2), new Point(22, 2), new Point(22, 3),
                new Point(22, 3), new Point(22, 3), new Point(22, 3), new Point(21, 3), new Point(21, 3),
                new Point(21, 3), new Point(21, 3), new Point(21, 3), new Point(21, 4), new Point(21, 4),
                new Point(21, 4), new Point(21, 4), new Point(21, 4), new Point(21, 4), new Point(21, 4),
                new Point(21, 4), new Point(21, 4), new Point(21, 4), new Point(21, 4), new Point(21, 5),
                new Point(21, 5), new Point(21, 5), new Point(21, 5), new Point(20, 5), new Point(20, 5),
                new Point(20, 5), new Point(20, 5), new Point(20, 5), new Point(20, 5), new Point(20, 6),
                new Point(20, 6), new Point(20, 6), new Point(20, 6), new Point(20, 6), new Point(20, 6),
                new Point(20, 6), new Point(20, 6), new Point(20, 6), new Point(20, 6), new Point(20, 6),
                new Point(20, 7), new Point(20, 7), new Point(20, 7), new Point(20, 7), new Point(20, 7),
                new Point(19, 7), new Point(19, 7), new Point(19, 7), new Point(19, 7), new Point(19, 7),
                new Point(19, 8), new Point(19, 8), new Point(19, 8), new Point(19, 8), new Point(19, 8),
                new Point(19, 8), new Point(19, 8), new Point(19, 8), new Point(19, 8), new Point(19, 8),
                new Point(19, 8), new Point(19, 9), new Point(19, 9), new Point(19, 9), new Point(19, 9),
                new Point(18, 9), new Point(18, 9), new Point(18, 9), new Point(18, 9), new Point(18, 9),
                new Point(18, 9), new Point(18, 10), new Point(18, 10), new Point(18, 10), new Point(18, 10),
                new Point(18, 10), new Point(18, 10), new Point(18, 10), new Point(18, 10), new Point(18, 10),
                new Point(18, 10), new Point(18, 11), new Point(18, 11), new Point(18, 11), new Point(18, 11),
                new Point(18, 11), new Point(18, 11), new Point(17, 11), new Point(17, 11), new Point(17, 11),
                new Point(17, 11), new Point(17, 12), new Point(17, 12), new Point(17, 12), new Point(17, 12),
                new Point(17, 12), new Point(17, 12), new Point(17, 12), new Point(17, 12), new Point(17, 12),
                new Point(17, 12), new Point(17, 12), new Point(17, 13), new Point(17, 13), new Point(17, 13),
                new Point(17, 13), new Point(17, 13), new Point(16, 13), new Point(16, 13), new Point(16, 13),
                new Point(16, 13), new Point(16, 13), new Point(16, 14), new Point(16, 14), new Point(16, 14),
                new Point(16, 14), new Point(16, 14), new Point(16, 14), new Point(16, 14), new Point(16, 14),
                new Point(16, 14), new Point(16, 14), new Point(16, 14), new Point(16, 15), new Point(16, 15),
                new Point(16, 15), new Point(16, 15), new Point(16, 15), new Point(16, 15), new Point(15, 15),
                new Point(15, 15), new Point(15, 15), new Point(15, 15), new Point(15, 16), new Point(15, 16),
                new Point(15, 16), new Point(15, 16), new Point(15, 16), new Point(15, 16), new Point(15, 16),
                new Point(15, 16), new Point(15, 16), new Point(15, 16), new Point(15, 17), new Point(15, 17),
                new Point(15, 17), new Point(15, 17), new Point(15, 17), new Point(15, 17), new Point(14, 17),
                new Point(14, 17), new Point(14, 17), new Point(14, 17), new Point(14, 18), new Point(14, 18),
                new Point(14, 18), new Point(14, 18), new Point(14, 18), new Point(14, 18), new Point(14, 18),
                new Point(14, 18), new Point(14, 18), new Point(14, 18), new Point(14, 18), new Point(14, 19),
                new Point(14, 19), new Point(14, 19), new Point(14, 19), new Point(14, 19), new Point(14, 19),
                new Point(14, 19), new Point(13, 19), new Point(13, 19), new Point(13, 19), new Point(13, 20),
                new Point(13, 20), new Point(13, 20), new Point(13, 20), new Point(13, 20), new Point(13, 20),
                new Point(13, 20), new Point(13, 20), new Point(13, 20), new Point(13, 20), new Point(13, 20),
                new Point(13, 21), new Point(13, 21), new Point(13, 21), new Point(13, 21), new Point(13, 21),
                new Point(13, 21), new Point(12, 21), new Point(12, 21), new Point(12, 21), new Point(12, 21),
                new Point(12, 22), new Point(12, 22), new Point(12, 22), new Point(12, 22), new Point(12, 22),
                new Point(12, 22), new Point(12, 22), new Point(12, 22), new Point(12, 22), new Point(12, 22),
                new Point(12, 22), new Point(12, 23), new Point(12, 23), new Point(12, 23), new Point(12, 23),
                new Point(12, 23), new Point(12, 23), new Point(12, 23), new Point(11, 23), new Point(11, 23),
                new Point(11, 24), new Point(11, 24), new Point(11, 24), new Point(11, 24), new Point(11, 24),
                new Point(11, 24), new Point(11, 24), new Point(11, 24), new Point(11, 24), new Point(11, 24),
                new Point(11, 24), new Point(11, 25), new Point(11, 25), new Point(11, 25), new Point(11, 25),
                new Point(11, 25), new Point(11, 25), new Point(11, 25), new Point(10, 25), new Point(10, 25),
                new Point(10, 25), new Point(10, 26), new Point(10, 26), new Point(10, 26), new Point(10, 26),
                new Point(10, 26), new Point(10, 26), new Point(10, 26), new Point(10, 26), new Point(10, 26),
                new Point(10, 26), new Point(10, 27), new Point(10, 27), new Point(10, 27), new Point(10, 27),
                new Point(9, 27), new Point(9, 27), new Point(9, 27), new Point(9, 26), new Point(9, 26),
                new Point(9, 26), new Point(8, 26), new Point(8, 26), new Point(8, 27), new Point(8, 27),
                new Point(8, 27), new Point(8, 27), new Point(8, 27), new Point(8, 27), new Point(8, 27),
                new Point(8, 27), new Point(8, 28), new Point(9, 28), new Point(9, 28), new Point(9, 28),
                new Point(9, 28), new Point(9, 28), new Point(9, 28), new Point(9, 28), new Point(8, 29),
                new Point(8, 29), new Point(8, 29), new Point(8, 29), new Point(7, 30), new Point(7, 30),
                new Point(7, 30), new Point(7, 30), new Point(7, 30), new Point(7, 30), new Point(7, 31),
                new Point(7, 31), new Point(7, 31), new Point(6, 31), new Point(6, 31), new Point(6, 31),
                new Point(6, 31), new Point(6, 31), new Point(6, 32), new Point(6, 32), new Point(6, 32),
                new Point(6, 32), new Point(5, 32), new Point(5, 32), new Point(5, 32), new Point(5, 33),
                new Point(5, 33), new Point(5, 33), new Point(5, 33), new Point(5, 33), new Point(5, 33),
                new Point(5, 33), new Point(4, 33), new Point(4, 34), new Point(4, 34), new Point(4, 34),
                new Point(4, 34), new Point(4, 34), new Point(5, 34), new Point(5, 34), new Point(4, 34),
                new Point(4, 34), new Point(4, 34), new Point(4, 34), new Point(4, 35), new Point(4, 35),
                new Point(4, 35), new Point(4, 35), new Point(4, 35), new Point(3, 35), new Point(3, 35),
                new Point(3, 35), new Point(3, 36), new Point(3, 36), new Point(3, 36), new Point(4, 36),
                new Point(4, 36), new Point(3, 36), new Point(3, 36), new Point(3, 36), new Point(3, 36),
                new Point(3, 36), new Point(3, 37), new Point(3, 37), new Point(3, 37), new Point(3, 37),
                new Point(2, 37), new Point(2, 37), new Point(2, 37), new Point(2, 37), new Point(2, 38),
                new Point(2, 38), new Point(2, 38), new Point(2, 38), new Point(2, 38), new Point(2, 38),
                new Point(2, 38), new Point(2, 39), new Point(2, 39), new Point(2, 39), new Point(2, 39),
                new Point(2, 39), new Point(2, 39), new Point(2, 39), new Point(2, 39), new Point(2, 39),
                new Point(2, 39), new Point(1, 40), new Point(1, 40), new Point(2, 40), new Point(2, 40),
                new Point(2, 40), new Point(2, 40), new Point(1, 40), new Point(1, 40), new Point(1, 40),
                new Point(1, 41), new Point(1, 41), new Point(1, 41), new Point(1, 41), new Point(1, 41),
                new Point(1, 41), new Point(1, 41), new Point(1, 42), new Point(1, 42), new Point(1, 42),
                new Point(1, 42), new Point(1, 42), new Point(1, 42), new Point(1, 42), new Point(0, 42),
                new Point(0, 43), new Point(0, 43), new Point(0, 43), new Point(1, 43), new Point(1, 43),
                new Point(1, 43), new Point(1, 43), new Point(1, 43), new Point(1, 43), new Point(1, 43),
                new Point(1, 43), new Point(2, 43), new Point(2, 43), new Point(2, 43), new Point(2, 44),
                new Point(2, 44), new Point(2, 44), new Point(2, 44), new Point(2, 44), new Point(3, 44),
                new Point(3, 44), new Point(3, 44), new Point(3, 44), new Point(3, 44), new Point(3, 44),
                new Point(3, 44), new Point(3, 44), new Point(4, 44), new Point(4, 44), new Point(4, 44),
                new Point(4, 44), new Point(4, 44), new Point(4, 44), new Point(4, 44), new Point(3, 45),
                new Point(3, 45), new Point(3, 45), new Point(3, 45), new Point(2, 45), new Point(2, 45),
                new Point(2, 45), new Point(2, 45), new Point(2, 45), new Point(1, 45), new Point(1, 45),
                new Point(1, 45), new Point(1, 52), new Point(1, 52), new Point(1, 52), new Point(1, 52),
                new Point(1, 52), new Point(2, 52), new Point(2, 52), new Point(2, 52), new Point(2, 52),
                new Point(2, 52), new Point(2, 52), new Point(3, 52), new Point(3, 52), new Point(3, 52),
                new Point(3, 52), new Point(3, 52), new Point(3, 52), new Point(4, 52), new Point(4, 53),
                new Point(4, 53), new Point(4, 53), new Point(4, 53), new Point(4, 53), new Point(4, 53),
                new Point(4, 53), new Point(4, 53), new Point(3, 53), new Point(3, 53), new Point(3, 53),
                new Point(3, 53), new Point(3, 53), new Point(3, 53), new Point(3, 53), new Point(3, 53),
                new Point(2, 53), new Point(2, 53), new Point(2, 54), new Point(2, 54), new Point(2, 54),
                new Point(2, 54), new Point(2, 54), new Point(2, 54), new Point(2, 54), new Point(1, 54),
                new Point(1, 54), new Point(1, 54), new Point(1, 54), new Point(1, 54), new Point(1, 54),
                new Point(1, 54), new Point(1, 54), new Point(0, 54), new Point(0, 54), new Point(0, 54),
                new Point(0, 54), new Point(0, 54), new Point(0, 54), new Point(0, 55), new Point(0, 55),
                new Point(0, 55), new Point(1, 56), new Point(0, 56), new Point(0, 56), new Point(0, 56),
                new Point(0, 56), new Point(0, 56), new Point(1, 56), new Point(1, 57), new Point(1, 57),
                new Point(1, 57), new Point(1, 58), new Point(1, 58), new Point(1, 58), new Point(1, 58),
                new Point(1, 58), new Point(1, 58), new Point(1, 58), new Point(1, 59), new Point(1, 59),
                new Point(1, 59), new Point(1, 59), new Point(1, 59), new Point(1, 59), new Point(1, 60),
                new Point(1, 60), new Point(1, 60), new Point(2, 60), new Point(2, 60), new Point(2, 60),
                new Point(2, 60), new Point(2, 60), new Point(2, 61), new Point(2, 61), new Point(2, 61),
                new Point(2, 61), new Point(2, 61), new Point(2, 61), new Point(2, 61), new Point(2, 61),
                new Point(2, 61), new Point(2, 61), new Point(2, 62), new Point(2, 62), new Point(2, 62),
                new Point(2, 62), new Point(2, 62), new Point(2, 62), new Point(2, 62), new Point(2, 62),
                new Point(2, 62), new Point(2, 62), new Point(3, 62), new Point(3, 63), new Point(3, 63),
                new Point(3, 63), new Point(3, 63), new Point(3, 63), new Point(3, 63), new Point(3, 63),
                new Point(3, 64), new Point(3, 64), new Point(3, 64), new Point(3, 64), new Point(4, 64),
                new Point(4, 64), new Point(4, 64), new Point(4, 64), new Point(4, 65), new Point(4, 65),
                new Point(4, 65), new Point(4, 65), new Point(4, 65), new Point(4, 65), new Point(4, 65),
                new Point(4, 65), new Point(4, 66), new Point(4, 66), new Point(4, 66), new Point(4, 66),
                new Point(5, 66), new Point(5, 66), new Point(5, 66), new Point(5, 66), new Point(5, 66),
                new Point(5, 66), new Point(5, 66), new Point(5, 67), new Point(5, 67), new Point(5, 67),
                new Point(5, 67), new Point(5, 67), new Point(5, 67), new Point(5, 67), new Point(5, 67),
                new Point(6, 68), new Point(6, 68), new Point(6, 68), new Point(6, 68), new Point(6, 68),
                new Point(6, 68), new Point(6, 68), new Point(6, 69), new Point(6, 69), new Point(7, 69),
                new Point(7, 69), new Point(7, 69), new Point(7, 69), new Point(8, 69), new Point(8, 70),
                new Point(8, 70), new Point(8, 70), new Point(8, 70), new Point(8, 70), new Point(8, 70),
                new Point(9, 71), new Point(9, 71), new Point(8, 72), new Point(9, 72), new Point(9, 72),
                new Point(10, 73), new Point(10, 73), new Point(10, 73), new Point(10, 73), new Point(11, 74),
                new Point(11, 74), new Point(11, 74), new Point(11, 73), new Point(11, 73), new Point(11, 73),
                new Point(11, 73), new Point(11, 74), new Point(11, 74), new Point(11, 74), new Point(11, 74),
                new Point(11, 74), new Point(11, 74), new Point(12, 74), new Point(12, 74), new Point(12, 75),
                new Point(12, 75), new Point(12, 75), new Point(12, 75), new Point(12, 75), new Point(13, 75),
                new Point(13, 75), new Point(13, 75), new Point(13, 75), new Point(13, 75), new Point(13, 75),
                new Point(13, 75), new Point(14, 76), new Point(14, 76), new Point(14, 76), new Point(14, 76),
                new Point(14, 75), new Point(14, 75), new Point(14, 75), new Point(14, 75), new Point(14, 76),
                new Point(14, 76), new Point(14, 76), new Point(14, 76), new Point(14, 76), new Point(15, 76),
                new Point(15, 76), new Point(15, 76), new Point(15, 76), new Point(15, 76), new Point(15, 76),
                new Point(15, 77), new Point(15, 77), new Point(15, 77), new Point(16, 77), new Point(16, 77),
                new Point(16, 77), new Point(16, 77), new Point(16, 77), new Point(16, 77), new Point(16, 77),
                new Point(16, 77), new Point(16, 77), new Point(16, 77), new Point(16, 77), new Point(17, 77),
                new Point(17, 77), new Point(17, 77), new Point(17, 77), new Point(17, 77), new Point(17, 77),
                new Point(17, 77), new Point(17, 77), new Point(17, 77), new Point(17, 77), new Point(18, 77),
                new Point(18, 77), new Point(18, 77), new Point(18, 77), new Point(18, 77), new Point(17, 78),
                new Point(18, 78), new Point(18, 78), new Point(18, 78), new Point(18, 78), new Point(18, 78),
                new Point(18, 78), new Point(18, 78), new Point(18, 78), new Point(18, 78), new Point(19, 78),
                new Point(19, 78), new Point(19, 78), new Point(19, 78), new Point(19, 78), new Point(19, 78),
                new Point(19, 78), new Point(20, 78), new Point(20, 78), new Point(20, 78), new Point(20, 78),
                new Point(20, 79), new Point(20, 79), new Point(20, 79), new Point(20, 79), new Point(20, 78),
                new Point(20, 78), new Point(20, 78), new Point(21, 78), new Point(21, 78), new Point(21, 78),
                new Point(21, 78), new Point(21, 79), new Point(21, 79), new Point(21, 79), new Point(21, 79),
                new Point(21, 79), new Point(21, 79), new Point(21, 79), new Point(22, 79), new Point(22, 79),
                new Point(22, 79), new Point(22, 79), new Point(22, 79), new Point(23, 79), new Point(23, 79),
                new Point(23, 79), new Point(23, 79), new Point(23, 79), new Point(24, 79), new Point(24, 80),
                new Point(24, 80), new Point(24, 79), new Point(24, 79), new Point(24, 79), new Point(24, 79),
                new Point(24, 79), new Point(24, 79), new Point(24, 80), new Point(25, 80), new Point(25, 80),
                new Point(26, 80), new Point(26, 80), new Point(26, 80), new Point(26, 80), new Point(27, 80),
                new Point(27, 80), new Point(27, 80), new Point(27, 80), new Point(28, 79), new Point(28, 79),
                new Point(28, 79), new Point(28, 80), new Point(31, 80), new Point(31, 80), new Point(31, 79),
                new Point(31, 79), new Point(32, 80), new Point(32, 80), new Point(32, 80), new Point(32, 80),
                new Point(32, 80), new Point(33, 80), new Point(33, 80), new Point(34, 80), new Point(34, 80),
                new Point(35, 80), new Point(35, 80), new Point(35, 79), new Point(35, 79), new Point(35, 79),
                new Point(35, 79), new Point(35, 79), new Point(35, 79), new Point(35, 80), new Point(36, 80),
                new Point(36, 79), new Point(36, 79), new Point(36, 79), new Point(36, 79), new Point(37, 79),
                new Point(37, 79), new Point(37, 79), new Point(37, 79), new Point(37, 79), new Point(38, 79),
                new Point(38, 79), new Point(38, 79), new Point(38, 79), new Point(38, 79), new Point(38, 79),
                new Point(38, 79), new Point(38, 78), new Point(38, 78), new Point(39, 78), new Point(39, 78),
                new Point(39, 78), new Point(39, 78), new Point(39, 79), new Point(39, 79), new Point(39, 79),
                new Point(39, 79), new Point(39, 79), new Point(39, 78), new Point(40, 78), new Point(40, 78),
                new Point(40, 78), new Point(40, 78), new Point(40, 78), new Point(40, 78), new Point(40, 78),
                new Point(40, 78), new Point(41, 78), new Point(41, 78), new Point(41, 78), new Point(41, 78),
                new Point(41, 78), new Point(41, 78), new Point(41, 78), new Point(41, 78), new Point(42, 78),
                new Point(42, 78), new Point(42, 78), new Point(42, 78), new Point(42, 77), new Point(42, 77),
                new Point(42, 77), new Point(42, 77), new Point(42, 77), new Point(42, 77), new Point(42, 77),
                new Point(42, 77), new Point(42, 77), new Point(42, 77), new Point(42, 77), new Point(42, 77),
                new Point(42, 77), new Point(42, 77), new Point(43, 77), new Point(43, 77), new Point(43, 77),
                new Point(43, 77), new Point(43, 77), new Point(43, 77), new Point(43, 77), new Point(43, 77),
                new Point(43, 77), new Point(43, 77), new Point(44, 77), new Point(44, 77), new Point(44, 76),
                new Point(44, 76), new Point(44, 76), new Point(44, 76), new Point(44, 76), new Point(44, 76),
                new Point(45, 76), new Point(45, 76), new Point(45, 76), new Point(45, 76), new Point(45, 76),
                new Point(45, 76), new Point(45, 76), new Point(45, 76), new Point(45, 75), new Point(45, 75),
                new Point(45, 75), new Point(45, 76), new Point(45, 76), new Point(45, 76), new Point(45, 76),
                new Point(46, 76), new Point(46, 76), new Point(46, 76), new Point(46, 75), new Point(46, 75),
                new Point(46, 75), new Point(46, 75), new Point(46, 75), new Point(47, 75), new Point(47, 75),
                new Point(47, 75), new Point(47, 75), new Point(47, 75), new Point(47, 74), new Point(47, 74),
                new Point(48, 74), new Point(48, 74), new Point(48, 74), new Point(48, 74), new Point(48, 74),
                new Point(48, 74), new Point(48, 73), new Point(48, 73), new Point(48, 74), new Point(48, 74),
                new Point(48, 74), new Point(49, 74), new Point(49, 74), new Point(49, 73), new Point(49, 73),
                new Point(50, 73), new Point(50, 73), new Point(51, 72), new Point(51, 72), new Point(50, 71),
                new Point(50, 71), new Point(50, 71), new Point(51, 71), new Point(51, 71), new Point(51, 70),
                new Point(52, 70), new Point(52, 70), new Point(52, 70), new Point(52, 70), new Point(52, 70),
                new Point(52, 70), new Point(52, 69), new Point(52, 69), new Point(52, 69), new Point(52, 69),
                new Point(52, 69), new Point(52, 69), new Point(53, 69), new Point(53, 69), new Point(53, 68),
                new Point(53, 68), new Point(53, 68), new Point(53, 68), new Point(53, 68), new Point(53, 68),
                new Point(54, 68), new Point(54, 68), new Point(54, 67), new Point(54, 67), new Point(54, 67),
                new Point(54, 67), new Point(54, 67), new Point(54, 67), new Point(54, 66), new Point(54, 66),
                new Point(54, 66), new Point(54, 66), new Point(55, 66), new Point(55, 66), new Point(55, 66),
                new Point(55, 66), new Point(55, 66), new Point(55, 66), new Point(55, 66), new Point(55, 66),
                new Point(55, 66), new Point(55, 65), new Point(55, 65), new Point(55, 65), new Point(55, 65),
                new Point(55, 65), new Point(56, 65), new Point(56, 65), new Point(56, 65), new Point(56, 64),
                new Point(56, 64), new Point(56, 64), new Point(56, 64), new Point(56, 64), new Point(56, 64),
                new Point(56, 64), new Point(56, 64), new Point(56, 64), new Point(56, 64), new Point(56, 64),
                new Point(56, 63), new Point(56, 63), new Point(56, 63), new Point(56, 63), new Point(56, 63),
                new Point(56, 63), new Point(56, 63), new Point(57, 63), new Point(57, 62), new Point(57, 62),
                new Point(57, 62), new Point(57, 62), new Point(57, 62), new Point(57, 62), new Point(57, 62),
                new Point(57, 62), new Point(57, 62), new Point(57, 62), new Point(57, 61), new Point(57, 61),
                new Point(57, 61), new Point(57, 61), new Point(57, 61), new Point(57, 61), new Point(57, 61),
                new Point(57, 61), new Point(58, 61), new Point(58, 60), new Point(57, 60), new Point(57, 60),
                new Point(58, 60), new Point(58, 60), new Point(58, 60), new Point(58, 59), new Point(58, 59),
                new Point(58, 59), new Point(58, 59), new Point(58, 59), new Point(58, 59), new Point(58, 58),
                new Point(58, 58), new Point(58, 58), new Point(58, 58), new Point(58, 58), new Point(58, 58),
                new Point(58, 58), new Point(58, 58), new Point(59, 57), new Point(59, 57), new Point(58, 57),
                new Point(58, 57), new Point(59, 57), new Point(59, 57), new Point(59, 57), new Point(59, 56),
                new Point(59, 56), new Point(59, 56), new Point(59, 56), new Point(59, 56), new Point(59, 56),
                new Point(59, 56), new Point(59, 56), new Point(59, 55), new Point(59, 55), new Point(59, 55),
                new Point(59, 55), new Point(59, 55), new Point(59, 55), new Point(59, 54), new Point(59, 54),
                new Point(59, 54), new Point(59, 54), new Point(59, 54), new Point(58, 54), new Point(58, 54),
                new Point(58, 54), new Point(58, 54), new Point(58, 54), new Point(58, 54), new Point(58, 54),
                new Point(58, 54), new Point(58, 54), new Point(57, 54), new Point(57, 54), new Point(57, 54),
                new Point(57, 54), new Point(57, 54), new Point(57, 54), new Point(57, 54), new Point(57, 54),
                new Point(56, 53), new Point(56, 53), new Point(56, 53), new Point(56, 53), new Point(56, 53),
                new Point(56, 53), new Point(56, 53), new Point(56, 53), new Point(56, 53), new Point(55, 53),
                new Point(55, 53), new Point(55, 53), new Point(55, 53), new Point(56, 53), new Point(56, 53),
                new Point(56, 53), new Point(56, 52), new Point(56, 52), new Point(56, 52), new Point(57, 52),
                new Point(57, 52), new Point(57, 52), new Point(57, 52), new Point(57, 52), new Point(57, 52),
                new Point(58, 52), new Point(58, 52), new Point(58, 52), new Point(58, 52), new Point(58, 52),
                new Point(58, 49), new Point(58, 49), new Point(58, 49), new Point(58, 49), new Point(58, 49),
                new Point(58, 49), new Point(58, 48), new Point(58, 48), new Point(58, 48), new Point(58, 48),
                new Point(58, 48), new Point(58, 48), new Point(58, 48), new Point(58, 48), new Point(58, 47),
                new Point(58, 47), new Point(58, 47), new Point(58, 47), new Point(58, 47), new Point(58, 47),
                new Point(58, 45), new Point(58, 45), new Point(58, 45), new Point(58, 45), new Point(58, 45),
                new Point(58, 45), new Point(58, 45), new Point(57, 45), new Point(57, 45), new Point(57, 45),
                new Point(57, 45), new Point(56, 45), new Point(56, 45), new Point(56, 45), new Point(56, 45),
                new Point(55, 45), new Point(55, 45), new Point(55, 44), new Point(55, 44), new Point(56, 44),
                new Point(56, 44), new Point(56, 44), new Point(56, 44), new Point(56, 44), new Point(56, 44),
                new Point(56, 44), new Point(56, 44), new Point(57, 44), new Point(57, 44), new Point(57, 44),
                new Point(57, 44), new Point(57, 44), new Point(57, 44), new Point(57, 44), new Point(58, 44),
                new Point(58, 44), new Point(58, 43), new Point(58, 43), new Point(58, 43), new Point(58, 43),
                new Point(58, 43), new Point(58, 43), new Point(58, 43), new Point(59, 43), new Point(59, 43),
                new Point(59, 43), new Point(59, 43), new Point(59, 43), new Point(59, 43), new Point(59, 42),
                new Point(59, 42), new Point(58, 42), new Point(58, 42), new Point(58, 42), new Point(58, 42),
                new Point(58, 42), new Point(58, 41), new Point(58, 41), new Point(58, 41), new Point(58, 41),
                new Point(58, 41), new Point(58, 41), new Point(58, 40), new Point(58, 40), new Point(58, 40),
                new Point(58, 40), new Point(57, 40), new Point(57, 40), new Point(58, 40), new Point(58, 40),
                new Point(58, 40), new Point(58, 39), new Point(58, 39), new Point(58, 39), new Point(57, 39),
                new Point(57, 39), new Point(57, 39), new Point(57, 39), new Point(57, 39), new Point(57, 39),
                new Point(57, 39), new Point(57, 39), new Point(57, 38), new Point(57, 38), new Point(57, 38),
                new Point(57, 38), new Point(57, 38), new Point(57, 38), new Point(57, 38), new Point(57, 37),
                new Point(56, 37), new Point(56, 37), new Point(56, 37), new Point(56, 37), new Point(56, 37),
                new Point(56, 37), new Point(56, 37), new Point(56, 36), new Point(56, 36), new Point(56, 36),
                new Point(56, 36), new Point(56, 36), new Point(56, 36), new Point(56, 36), new Point(57, 36),
                new Point(57, 36), new Point(57, 36), new Point(57, 36), new Point(57, 36), new Point(57, 35),
                new Point(57, 35), new Point(57, 35), new Point(57, 35), new Point(58, 35), new Point(58, 35),
                new Point(58, 35), new Point(58, 35), new Point(58, 35), new Point(58, 35), new Point(58, 35),
                new Point(58, 35), new Point(58, 35), new Point(59, 35), new Point(59, 34), new Point(59, 34),
                new Point(59, 34), new Point(59, 34), new Point(59, 34), new Point(59, 34), new Point(59, 28),
                new Point(59, 28), new Point(59, 28), new Point(59, 28), new Point(59, 28), new Point(59, 28),
                new Point(59, 26), new Point(59, 26), new Point(59, 26), new Point(59, 26), new Point(59, 26),
                new Point(59, 26), new Point(59, 25), new Point(59, 25), new Point(59, 25), new Point(59, 25),
                new Point(59, 25), new Point(59, 25), new Point(59, 25), new Point(59, 24), new Point(59, 24),
                new Point(59, 24), new Point(59, 24), new Point(58, 24), new Point(58, 24), new Point(58, 24),
                new Point(58, 23), new Point(58, 23), new Point(58, 23), new Point(58, 23), new Point(58, 23),
                new Point(58, 23), new Point(58, 22), new Point(58, 22), new Point(58, 22), new Point(58, 22),
                new Point(58, 22), new Point(58, 22), new Point(58, 22), new Point(58, 21), new Point(58, 21),
                new Point(58, 21), new Point(58, 21), new Point(57, 21), new Point(57, 21), new Point(57, 21),
                new Point(57, 21), new Point(57, 21), new Point(57, 21), new Point(57, 21), new Point(53, 21),
                new Point(53, 21), new Point(53, 21), new Point(53, 21), new Point(53, 22), new Point(53, 22),
                new Point(53, 22), new Point(53, 22), new Point(53, 22), new Point(53, 22), new Point(53, 23),
                new Point(53, 23), new Point(53, 23), new Point(53, 23), new Point(53, 23), new Point(53, 23),
                new Point(52, 23), new Point(52, 24), new Point(52, 24), new Point(52, 24), new Point(52, 24),
                new Point(52, 24), new Point(52, 24), new Point(52, 24), new Point(52, 25), new Point(52, 25),
                new Point(52, 25), new Point(52, 25), new Point(52, 25), new Point(52, 25), new Point(52, 26),
                new Point(52, 26), new Point(52, 26), new Point(52, 26), new Point(52, 26), new Point(52, 26),
                new Point(52, 26), new Point(52, 29), new Point(51, 29), new Point(51, 29), new Point(51, 29),
                new Point(51, 29), new Point(51, 29), new Point(51, 29), new Point(51, 28), new Point(51, 28),
                new Point(50, 28), new Point(50, 28), new Point(50, 27), new Point(50, 27), new Point(50, 27),
                new Point(50, 27), new Point(49, 27), new Point(49, 26), new Point(49, 26), new Point(49, 26),
                new Point(49, 26), new Point(49, 26), new Point(49, 26), new Point(49, 26), new Point(49, 26),
                new Point(49, 26), new Point(49, 26), new Point(49, 26), new Point(49, 25), new Point(49, 25),
                new Point(49, 25), new Point(49, 25), new Point(49, 25), new Point(49, 25), new Point(48, 25),
                new Point(48, 25), new Point(48, 25), new Point(48, 25), new Point(48, 24), new Point(48, 24),
                new Point(48, 24), new Point(48, 24), new Point(48, 24), new Point(48, 24), new Point(48, 24),
                new Point(48, 24), new Point(48, 24), new Point(48, 24), new Point(48, 23), new Point(48, 23),
                new Point(48, 23), new Point(48, 23), new Point(48, 23), new Point(48, 23), new Point(48, 23),
                new Point(48, 23), new Point(47, 23), new Point(47, 23), new Point(47, 22), new Point(47, 22),
                new Point(47, 22), new Point(47, 22), new Point(47, 22), new Point(47, 22), new Point(47, 22),
                new Point(47, 22), new Point(47, 22), new Point(47, 22), new Point(47, 22), new Point(47, 21),
                new Point(47, 21), new Point(47, 21), new Point(47, 21), new Point(47, 21), new Point(47, 21),
                new Point(47, 21), new Point(46, 21), new Point(46, 21), new Point(46, 20), new Point(46, 20),
                new Point(46, 20), new Point(46, 20), new Point(46, 20), new Point(46, 20), new Point(46, 20),
                new Point(46, 20), new Point(46, 20), new Point(46, 20), new Point(46, 20), new Point(46, 19),
                new Point(46, 19), new Point(46, 19), new Point(46, 19), new Point(46, 19), new Point(46, 19),
                new Point(46, 19), new Point(46, 19), new Point(46, 19), new Point(45, 19), new Point(45, 18),
                new Point(45, 18), new Point(45, 18), new Point(45, 18), new Point(45, 18), new Point(45, 18),
                new Point(45, 18), new Point(45, 18), new Point(45, 18), new Point(45, 18), new Point(45, 18),
                new Point(45, 17), new Point(45, 17), new Point(45, 17), new Point(45, 17), new Point(45, 17),
                new Point(45, 17), new Point(45, 17), new Point(45, 17), new Point(44, 17), new Point(44, 17),
                new Point(44, 16), new Point(44, 16), new Point(44, 16), new Point(44, 16), new Point(44, 16),
                new Point(44, 16), new Point(44, 16), new Point(44, 16), new Point(44, 16), new Point(44, 16),
                new Point(44, 15), new Point(44, 15), new Point(44, 15), new Point(44, 15), new Point(44, 15),
                new Point(44, 15), new Point(44, 15), new Point(44, 15), new Point(44, 15), new Point(44, 15),
                new Point(43, 14), new Point(43, 14), new Point(43, 14), new Point(43, 14), new Point(43, 14),
                new Point(43, 14), new Point(43, 14), new Point(43, 14), new Point(43, 14), new Point(43, 14),
                new Point(43, 14), new Point(43, 13), new Point(43, 13), new Point(43, 13), new Point(43, 13),
                new Point(43, 13), new Point(43, 13), new Point(43, 13), new Point(43, 13), new Point(43, 13),
                new Point(42, 13), new Point(42, 12), new Point(42, 12), new Point(42, 12), new Point(42, 12),
                new Point(42, 12), new Point(42, 12), new Point(42, 12), new Point(42, 12), new Point(42, 12),
                new Point(42, 12), new Point(42, 11), new Point(42, 11), new Point(42, 11), new Point(42, 11),
                new Point(42, 11), new Point(42, 11), new Point(42, 11), new Point(42, 11), new Point(42, 11),
                new Point(42, 11), new Point(42, 10), new Point(41, 10), new Point(41, 10), new Point(41, 10),
                new Point(41, 10), new Point(41, 10), new Point(41, 10), new Point(41, 10), new Point(41, 10),
                new Point(41, 10), new Point(41, 10), new Point(41, 9), new Point(41, 9), new Point(41, 9),
                new Point(41, 9), new Point(41, 9), new Point(41, 9), new Point(41, 9), new Point(41, 9),
                new Point(41, 9), new Point(41, 9), new Point(40, 8), new Point(40, 8), new Point(40, 8),
                new Point(40, 8), new Point(40, 8), new Point(40, 8), new Point(40, 8), new Point(40, 8),
                new Point(40, 8), new Point(40, 8), new Point(40, 7), new Point(40, 7), new Point(40, 7),
                new Point(40, 7), new Point(40, 7), new Point(40, 7), new Point(40, 7), new Point(40, 7),
                new Point(40, 7), new Point(40, 7), new Point(40, 6), new Point(40, 6), new Point(39, 6),
                new Point(39, 6), new Point(39, 6), new Point(39, 6), new Point(39, 6), new Point(39, 6),
                new Point(39, 6), new Point(39, 6), new Point(39, 6), new Point(39, 5), new Point(39, 5),
                new Point(39, 5), new Point(39, 5), new Point(39, 5), new Point(39, 5), new Point(39, 5),
                new Point(39, 5), new Point(39, 5), new Point(39, 5), new Point(39, 4), new Point(38, 4),
                new Point(38, 4), new Point(38, 4), new Point(38, 4), new Point(38, 4), new Point(38, 4),
                new Point(38, 4), new Point(38, 4), new Point(38, 4), new Point(38, 3), new Point(38, 3),
                new Point(38, 3), new Point(38, 3), new Point(38, 3), new Point(38, 3), new Point(38, 3),
                new Point(38, 3), new Point(38, 3), new Point(38, 3), new Point(38, 2), new Point(38, 2),
                new Point(38, 2), new Point(37, 2), new Point(37, 2), new Point(37, 2), new Point(37, 2),
                new Point(37, 2), new Point(37, 2), new Point(37, 2), new Point(37, 2), new Point(37, 1),
                new Point(37, 1), new Point(37, 1), new Point(37, 1), new Point(37, 1), new Point(37, 1),
                new Point(37, 1), new Point(37, 1), new Point(37, 1), new Point(37, 1), new Point(36, 0),
                new Point(35, 0), new Point(35, 0), new Point(35, 0), new Point(35, 0), new Point(35, 0),
                new Point(35, 0), new Point(34, 0), new Point(34, 0), new Point(34, 0), new Point(34, 0),
                new Point(34, 0), new Point(34, 2), new Point(34, 2), new Point(34, 2), new Point(34, 2),
                new Point(34, 3), new Point(33, 3), new Point(33, 4), new Point(33, 4), new Point(33, 13),
                new Point(33, 13), new Point(33, 13), new Point(34, 13), new Point(34, 13), new Point(34, 21),
                new Point(34, 21), new Point(34, 21), new Point(34, 21), new Point(34, 21), new Point(34, 14),
                new Point(26, 14), new Point(26, 14), new Point(26, 14), new Point(26, 15), new Point(26, 15),
                new Point(26, 15), new Point(26, 15), new Point(26, 16), new Point(26, 16), new Point(26, 16),
                new Point(26, 17), new Point(26, 20), new Point(26, 20), new Point(26, 20), new Point(26, 20),
                new Point(26, 21), new Point(26, 21), new Point(25, 21), new Point(25, 21), new Point(25, 21),
                new Point(25, 21), new Point(25, 13), new Point(25, 13), new Point(25, 13), new Point(26, 13),
                new Point(26, 13), new Point(26, 4), new Point(26, 3), new Point(26, 3), new Point(26, 3),
                new Point(26, 0)
            };

        private static Point[] myNave1_Cabina = {
        new Point(28, 5), new Point(32, 5), new Point(32, 20), new Point(28, 20)
    };

        private static Point[] myNave1_MotorLuz = {
        new Point(20, 75), new Point(40, 75), new Point(40, 80), new Point(20, 80)
    };

        private static Point[] myNave2 = { new Point(29, 8), new Point(29, 5), new Point(29, 18), new Point(32, 21), new Point(34, 21), new Point(38, 17), new Point(41, 20), new Point(41, 30), new Point(47, 36), new Point(47, 41), new Point(41, 41), new Point(41, 38), new Point(38, 36), new Point(36, 33), new Point(35, 33), new Point(33, 34), new Point(30, 32), new Point(19, 9), new Point(8, 3), new Point(7, 9), new Point(6, 9), new Point(7, 8), new Point(8, 6), new Point(9, 6), new Point(10, 5), new Point(11, 1), new Point(12, 3), new Point(13, 1), new Point(13, 21), new Point(13, 21), new Point(15, 11), new Point(16, 0), new Point(17, 0) };

        private static Point[] myNave3 = { new Point(26, 9), new Point(25, 9), new Point(24, 9), new Point(23, 9), new Point(22, 9), new Point(22, 17), new Point(23, 17), new Point(24, 18), new Point(25, 19), new Point(26, 20), new Point(27, 21), new Point(28, 22), new Point(29, 22), new Point(30, 23), new Point(31, 23), new Point(32, 22), new Point(33, 22), new Point(34, 21), new Point(35, 20), new Point(36, 19), new Point(37, 18), new Point(38, 17), new Point(39, 16), new Point(40, 16), new Point(41, 15), new Point(42, 14), new Point(43, 13), new Point(44, 12), new Point(45, 11), new Point(46, 11), new Point(47, 10), new Point(48, 9), new Point(49, 8), new Point(50, 7), new Point(51, 6), new Point(52, 5), new Point(53, 5), new Point(54, 4), new Point(55, 3), new Point(56, 3), new Point(57, 2), new Point(58, 1), new Point(59, 1), new Point(60, 0), new Point(61, 0), new Point(62, 1), new Point(63, 1), new Point(64, 2), new Point(65, 3), new Point(66, 3), new Point(67, 4), new Point(68, 5), new Point(69, 5), new Point(70, 6), new Point(71, 7), new Point(72, 8), new Point(73, 9), new Point(74, 10), new Point(75, 11), new Point(76, 11), new Point(77, 12), new Point(78, 13), new Point(79, 14), new Point(80, 15), new Point(81, 16), new Point(82, 16), new Point(83, 17), new Point(84, 18), new Point(85, 19), new Point(86, 20), new Point(87, 21), new Point(88, 22), new Point(89, 22), new Point(90, 23), new Point(91, 23), new Point(92, 22), new Point(93, 22), new Point(94, 21), new Point(95, 20), new Point(96, 19), new Point(97, 18), new Point(98, 17), new Point(99, 16), new Point(100, 16), new Point(101, 15), new Point(102, 14), new Point(103, 13), new Point(104, 12), new Point(105, 11), new Point(106, 11), new Point(107, 10), new Point(108, 9), new Point(109, 8), new Point(110, 7), new Point(111, 6), new Point(112, 5), new Point(113, 5), new Point(114, 4), new Point(115, 3), new Point(116, 3), new Point(117, 2), new Point(118, 1), new Point(119, 1), new Point(120, 0), new Point(121, 0), new Point(122, 1), new Point(123, 1), new Point(124, 2), new Point(125, 3), new Point(126, 3), new Point(127, 4), new Point(128, 5), new Point(129, 5), new Point(130, 6), new Point(131, 7), new Point(132, 8), new Point(133, 9), new Point(134, 10), new Point(135, 11), new Point(136, 11), new Point(137, 12), new Point(138, 13), new Point(139, 14), new Point(140, 15), new Point(141, 16), new Point(142, 16), new Point(143, 17), new Point(144, 18), new Point(145, 19), new Point(146, 20), new Point(147, 21), new Point(148, 22), new Point(149, 22), new Point(150, 23), new Point(151, 23), new Point(152, 22), new Point(153, 22), new Point(154, 21), new Point(155, 20), new Point(156, 19), new Point(157, 18), new Point(158, 17), new Point(159, 16), new Point(160, 16), new Point(161, 15), new Point(162, 14), new Point(163, 13), new Point(164, 12), new Point(165, 11), new Point(166, 11), new Point(167, 10), new Point(168, 9), new Point(169, 8), new Point(170, 7), new Point(171, 6), new Point(172, 5), new Point(173, 5), new Point(174, 4), new Point(175, 3), new Point(176, 3), new Point(177, 2), new Point(178, 1), new Point(179, 1), new Point(180, 0), new Point(181, 0), new Point(182, 1), new Point(183, 1), new Point(184, 2), new Point(185, 3), new Point(186, 3), new Point(187, 4), new Point(188, 5), new Point(189, 5), new Point(190, 6), new Point(191, 7), new Point(192, 8), new Point(193, 9), new Point(194, 10), new Point(195, 11), new Point(196, 11), new Point(197, 12), new Point(198, 13), new Point(199, 14), new Point(200, 15), new Point(201, 16), new Point(202, 16), new Point(203, 17), new Point(204, 18), new Point(205, 19), new Point(206, 20), new Point(207, 21), new Point(208, 22), new Point(209, 22), new Point(210, 23), new Point(211, 23), new Point(212, 22), new Point(213, 22), new Point(214, 21), new Point(215, 20), new Point(216, 19), new Point(217, 18), new Point(218, 17), new Point(219, 16), new Point(220, 16), new Point(221, 15), new Point(222, 14), new Point(223, 13), new Point(224, 12), new Point(225, 11), new Point(226, 11), new Point(227, 10), new Point(228, 9), new Point(229, 8), new Point(230, 7), new Point(231, 6), new Point(232, 5), new Point(233, 5), new Point(234, 4), new Point(235, 3), new Point(236, 3), new Point(237, 2), new Point(238, 1), new Point(239, 1), new Point(240, 0), new Point(241, 0), new Point(242, 1), new Point(243, 1), new Point(244, 2), new Point(245, 3), new Point(246, 3), new Point(247, 4), new Point(248, 5), new Point(249, 5), new Point(250, 6), new Point(251, 7), new Point(252, 8), new Point(253, 9), new Point(254, 10), new Point(255, 11), new Point(256, 11), new Point(257, 12), new Point(258, 13), new Point(259, 14), new Point(260, 15), new Point(261, 16), new Point(262, 16), new Point(263, 17), new Point(264, 18), new Point(265, 19), new Point(266, 20), new Point(267, 21), new Point(268, 22), new Point(269, 22), new Point(270, 23), new Point(271, 23), new Point(272, 22), new Point(273, 22), new Point(274, 21), new Point(275, 20), new Point(276, 19), new Point(277, 18), new Point(278, 17), new Point(279, 16), new Point(280, 16), new Point(281, 15), new Point(282, 14), new Point(283, 13), new Point(284, 12), new Point(285, 11), new Point(286, 11), new Point(287, 10), new Point(288, 9), new Point(289, 8), new Point(290, 7), new Point(291, 6), new Point(292, 5), new Point(293, 5), new Point(294, 4), new Point(295, 3), new Point(296, 3), new Point(297, 2), new Point(298, 1), new Point(299, 1), new Point(300, 0), new Point(301, 0) };

        public static void CreateShip(PictureBox Avion, int AngRotar, int Tipox, Color Pintar, int Vida)
        {
            int largoN = 1;
            int anchoN = 1;
            const int IMG_ORIGINAL_WIDTH = 347;
            const int IMG_ORIGINAL_HEIGHT = 470;
            Point[] selectedNavePoints = null;

            if (Tipox == 1)
            {
                largoN = 80;
                anchoN = 69;
                selectedNavePoints = myNave1;
            }
            else if (Tipox == 2)
            {
                largoN = 50;
                anchoN = 50;
                selectedNavePoints = myNave2;
            }
            else if (Tipox == 3)
            {
                largoN = 51;
                anchoN = 305;
                selectedNavePoints = myNave3;
            }
            Avion.Tag = Vida;

            if (selectedNavePoints != null)
            {
                Point[] myNave = new Point[selectedNavePoints.Length];
                for (int i = 0; i < selectedNavePoints.Length; i++)
                {
                    myNave[i].X = selectedNavePoints[i].X;
                    if (AngRotar == 180)
                        myNave[i].Y = largoN - 1 - selectedNavePoints[i].Y;
                    else
                        myNave[i].Y = selectedNavePoints[i].Y;
                }

                GraphicsPath ObjGrafico = new GraphicsPath();
                ObjGrafico.AddPolygon(myNave);

                Avion.Size = new Size(anchoN, largoN);
                Avion.Region = new Region(ObjGrafico);
                Avion.Location = new Point(0, 0);

                Bitmap Imagen = new Bitmap(Avion.Width, Avion.Height);
                using (Graphics PintaImg = Graphics.FromImage(Imagen))
                {
                    PintaImg.Clear(Color.Transparent);
                    PintaImg.SmoothingMode = SmoothingMode.None;

                    Brush primaryBrush;
                    bool useTexture = (Tipox == 1 && Resources.Nave1Texture != null);

                    if (Tipox == 1)
                    {
                        // 1. Capa: Cabina (Gris claro)
                        Point[] cabinaRotada = RotatePointsIfNecessary(myNave1_Cabina, AngRotar, largoN);
                        using (SolidBrush cabinaBrush = new SolidBrush(Color.Silver))
                        {
                            PintaImg.FillPolygon(cabinaBrush, cabinaRotada);
                        }

                        // 2. Capa: Motores/Luz (Naranja/Amarillo)
                        Point[] motorLuzRotada = RotatePointsIfNecessary(myNave1_MotorLuz, AngRotar, largoN);
                        using (SolidBrush luzBrush = new SolidBrush(Color.Orange))
                        {
                            PintaImg.FillPolygon(luzBrush, motorLuzRotada);
                        }

                        // Aquí podrías agregar más capas (alas, armas, sombras) usando sus respectivos polígonos.
                    }
                }
                Avion.Image = Imagen;
            }
            Avion.Visible = true;
        }

        //-------------EFECTOS DE LA NAVE PRINCIPAL-------------//
        public static void ShipRun(PictureBox Avion, int AngRotar, int velox)
        {
            // 1. Obtener la imagen base (sin efectos)
            Bitmap ImagenBase = (Avion.Image as Bitmap);
            if (ImagenBase == null)
            {
                // Si no hay imagen base (posiblemente CreateShip falló o no se llamó), salimos.
                return;
            }

            // Creamos una nueva imagen para dibujar los efectos encima.
            Bitmap ImagenConEfectos = new Bitmap(Avion.Width, Avion.Height);

            using (Graphics PintaImg = Graphics.FromImage(ImagenConEfectos))
            {
                // Dibujar la nave base (sin rotar)
                PintaImg.DrawImage(ImagenBase, new Point(0, 0));

                // Puntos de Efectos de la Nave (Asumo que son de la Nave Tipo 1)
                Point[] puntoDer = { new Point(35, 28), new Point(35, 30), new Point(36, 30), new Point(37, 31),
                new Point(37, 37), new Point(38, 38), new Point(38, 40), new Point(39, 41), new Point(39, 44), new Point(40, 45),
                new Point(40, 46), new Point(42, 48), new Point(43, 48), new Point(44, 49), new Point(44, 64), new Point(43, 65),
                new Point(42, 65), new Point(41, 66), new Point(40, 66), new Point(38, 68), new Point(36, 68), new Point(36, 69),
                new Point(36, 63), new Point(35, 62), new Point(35, 28) };
                Point[] puntoIzq = { new Point(23,28), new Point(23,30), new Point(22,30), new Point(21,31), new Point(21,37),
                new Point(20,36), new Point(20,40), new Point(19,41), new Point(19,44), new Point(18,45), new Point(18,46),
                new Point(16,48), new Point(15,48), new Point(14,49), new Point(14,64), new Point(15,65), new Point(16,65),
                new Point(17,66), new Point(18,66), new Point(20,68), new Point(22,68), new Point(22,69), new Point(22,63),
                new Point(23,62), new Point(23,28) };
                Point[] puntoAtr = { new Point(29, 21), new Point(31, 19), new Point(32, 19), new Point(33, 20), new Point(33, 25),
                new Point(33,26), new Point(32,63), new Point(34,65), new Point(34,68), new Point(33,69), new Point(33,74),
                new Point(32,73), new Point(31,73), new Point(29,71), new Point(27,73), new Point(26,73), new Point(25,74),
                new Point(25,69), new Point(24,68), new Point(24,65), new Point(26,63), new Point(26,26), new Point(25,25),
                new Point(25,20), new Point(26,19), new Point(27,19), new Point(29,21)};


                // --- LÓGICA DE VELOCIDAD/EFECTOS (Corregida) ---

                if (velox == 1) // Efectos de "Movimiento Lento / Reposo"
                {
                    // Ejemplo de dibujar un efecto de motor simple
                    PintaImg.FillRectangle(Brushes.Orange, 30, 75, 2, 5); // Fuego trasero central

                    // Dibujamos los polígonos de alas/cuerpo si son parte del efecto
                    // Por ejemplo, para resaltar las alas.
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(50, Color.White)))
                    {
                        PintaImg.FillPolygon(brush, puntoDer);
                        PintaImg.FillPolygon(brush, puntoIzq);
                    }
                }
                else if (velox == 2) // Efectos de "Aceleración / Escudo"
                {
                    // Dibujar los polígonos de efectos (como escudos) con un color diferente
                    using (SolidBrush shieldBrush = new SolidBrush(Color.FromArgb(150, Color.LightBlue)))
                    {
                        PintaImg.FillPolygon(shieldBrush, puntoDer);
                        PintaImg.FillPolygon(shieldBrush, puntoIzq);
                        PintaImg.FillPolygon(shieldBrush, puntoAtr);
                    }
                    // Dibujar llamas más intensas
                    PintaImg.FillRectangle(Brushes.Red, 30, 70, 2, 10);
                }
                // Los casos velox == 3 y los rectángulos originales no parecen estar relacionados
                // con los polígonos definidos. Si necesitas esos efectos de píxeles, úsalos:
                else if (velox == 3)
                {
                    // Mantengo tus píxeles de ejemplo, aunque están en posiciones bajas (1, 1) que no afectan la nave grande
                    PintaImg.FillRectangle(Brushes.DarkRed, 15, 30, 1, 1);
                    PintaImg.FillRectangle(Brushes.DarkRed, 25, 1, 1, 16);
                    PintaImg.FillRectangle(Brushes.DarkRed, 37, 1, 1, 9);
                }

                // 2. Asignar la imagen final y rotarla si es necesario
                Avion.Image = Utils.RotateImage(ImagenConEfectos, AngRotar);
            }
        }

        private static Point[] RotatePointsIfNecessary(Point[] originalPoints, int angle, int height)
        {
            if (angle == 180)
            {
                Point[] rotated = new Point[originalPoints.Length];
                for (int i = 0; i < originalPoints.Length; i++)
                {
                    // Mantenemos X, invertimos Y
                    rotated[i].X = originalPoints[i].X;
                    rotated[i].Y = height - 1 - originalPoints[i].Y;
                }
                return rotated;
            }
            return originalPoints; // No hay rotación
        }
    }
}
