﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoVisual.FloatingPoint
{
    public class ShowcaseListItem : NotImplementedLab.Data.ShowcaseListItem
    {
        public ShowcaseListItem() : base("Floating point numbers",
            "Red!Yellow|M 45.121094 75.5625 C 40.851562 77.832031 40.050781 80.765625 44.054688 79.699219 C 46.457031 79.03125 46.726562 79.699219 46.726562 85.574219 C 46.726562 91.179688 46.324219 92.113281 44.054688 92.113281 C 42.585938 92.113281 41.386719 92.648438 41.386719 93.449219 C 41.386719 94.117188 44.722656 94.785156 48.726562 94.785156 C 55.9375 94.785156 58.605469 93.050781 53.398438 91.714844 C 51.128906 91.179688 50.730469 89.84375 50.730469 82.234375 C 50.730469 77.429688 50.328125 73.425781 49.796875 73.425781 C 49.128906 73.558594 47.125 74.492188 45.121094 75.5625 Z M 45.121094 75.5625;" +
            "Yellow|M 63.144531 91.582031 C 62.210938 94.25 65.414062 96.519531 67.683594 94.652344 C 69.6875 92.914062 68.484375 89.445312 65.949219 89.445312 C 64.882812 89.445312 63.679688 90.378906 63.144531 91.582031 Z M 63.144531 91.582031;" +
            "Yellow|M 79.699219 75.5625 C 75.695312 77.695312 76.09375 80.765625 80.234375 79.433594 C 82.503906 78.632812 82.769531 79.300781 82.769531 84.773438 C 82.769531 89.84375 82.234375 91.179688 80.101562 91.714844 C 75.160156 93.050781 77.429688 94.785156 84.105469 94.785156 C 87.84375 94.785156 90.78125 94.117188 90.78125 93.449219 C 90.78125 92.648438 89.84375 92.113281 88.777344 92.113281 C 87.175781 92.113281 86.773438 90.246094 86.773438 82.769531 C 86.773438 72.625 86.242188 72.089844 79.699219 75.5625 Z M 79.699219 75.5625;" +
            "Yellow|M 97.054688 75.160156 C 91.847656 78.898438 93.183594 93.71875 98.789062 95.183594 C 102.660156 96.253906 107.335938 93.050781 108.535156 88.511719 C 111.472656 78.363281 104.128906 69.953125 97.054688 75.160156 Z M 104.128906 78.496094 C 104.128906 80.234375 98.921875 84.640625 97.988281 83.703125 C 96.386719 82.101562 99.058594 77.429688 101.59375 77.429688 C 102.929688 77.429688 104.128906 77.964844 104.128906 78.496094 Z M 104.128906 90.113281 C 103.195312 91.179688 101.59375 92.113281 100.660156 92.113281 C 97.722656 92.113281 98.523438 88.511719 101.859375 86.109375 C 104.53125 84.238281 104.796875 84.238281 105.199219 86.109375 C 105.464844 87.308594 104.929688 89.042969 104.128906 90.113281 Z M 104.128906 90.113281;" +
            "Yellow|M 113.609375 76.894531 C 109.738281 81.835938 110.003906 88.777344 114.140625 92.78125 C 115.878906 94.652344 118.015625 96.121094 118.816406 96.121094 C 119.617188 96.121094 121.753906 94.652344 123.488281 92.78125 C 129.761719 86.640625 126.691406 73.425781 118.949219 73.425781 C 117.480469 73.425781 115.078125 75.027344 113.609375 76.894531 Z M 121.484375 78.898438 C 121.484375 80.5 117.613281 83.972656 115.878906 84.105469 C 114.277344 84.105469 114.675781 80.765625 116.410156 79.03125 C 118.414062 77.03125 121.484375 76.894531 121.484375 78.898438 Z M 120.949219 90.511719 C 119.214844 92.25 118.546875 92.382812 117.214844 91.046875 C 115.746094 89.578125 116.144531 88.777344 118.816406 86.773438 C 121.753906 84.503906 122.152344 84.503906 122.554688 86.375 C 122.820312 87.441406 122.152344 89.445312 120.949219 90.511719 Z M 120.949219 90.511719 ;" +
            "Yellow|M 133.234375 75.5625 C 128.960938 77.832031 128.160156 80.765625 132.164062 79.699219 C 134.566406 79.03125 134.835938 79.699219 134.835938 85.574219 C 134.835938 91.179688 134.433594 92.113281 132.164062 92.113281 C 130.695312 92.113281 129.496094 92.648438 129.496094 93.449219 C 129.496094 94.117188 132.832031 94.785156 136.835938 94.785156 C 144.046875 94.785156 146.714844 93.050781 141.511719 91.714844 C 139.242188 91.179688 138.839844 89.84375 138.839844 82.234375 C 138.839844 77.429688 138.441406 73.425781 137.90625 73.425781 C 137.238281 73.558594 135.234375 74.492188 133.234375 75.5625 Z M 133.234375 75.5625 ;" +
            "Yellow|M 148.984375 75.960938 C 145.382812 79.964844 145.78125 90.648438 149.652344 93.71875 C 155.394531 98.523438 161.535156 93.851562 161.535156 84.503906 C 161.535156 75.160156 154.191406 70.222656 148.984375 75.960938 Z M 156.863281 78.765625 C 157.664062 80.101562 151.253906 84.90625 150.054688 83.703125 C 149.652344 83.304688 149.921875 81.703125 150.855469 80.234375 C 152.324219 77.296875 155.527344 76.496094 156.863281 78.765625 Z M 157.53125 87.441406 C 157.53125 91.179688 154.191406 93.316406 151.921875 91.046875 C 150.855469 89.980469 151.253906 88.777344 153.660156 86.910156 C 157.664062 83.4375 157.53125 83.4375 157.53125 87.441406 Z M 157.53125 87.441406 ;" +
            "Yellow|M 44.988281 108.9375 C 40.984375 111.070312 41.386719 114.140625 45.523438 112.808594 C 47.792969 112.007812 48.058594 112.675781 48.058594 118.148438 C 48.058594 123.21875 47.527344 124.554688 45.390625 125.089844 C 40.449219 126.425781 42.71875 128.160156 49.394531 128.160156 C 53.132812 128.160156 56.070312 127.492188 56.070312 126.824219 C 56.070312 126.023438 55.136719 125.488281 54.066406 125.488281 C 52.464844 125.488281 52.066406 123.621094 52.066406 116.144531 C 52.066406 106 51.53125 105.464844 44.988281 108.9375 Z M 44.988281 108.9375 ;" +
            "Yellow|M 63.144531 108.402344 C 59.273438 110.269531 59.007812 114.007812 62.746094 112.808594 C 65.148438 112.007812 65.414062 112.539062 65.414062 118.148438 C 65.414062 123.21875 64.882812 124.554688 62.746094 125.089844 C 57.804688 126.425781 60.074219 128.160156 66.75 128.160156 C 70.488281 128.160156 73.425781 127.492188 73.425781 126.824219 C 73.425781 126.023438 72.492188 125.488281 71.421875 125.488281 C 69.820312 125.488281 69.421875 123.621094 69.421875 116.144531 C 69.421875 106.265625 68.886719 105.597656 63.144531 108.402344 Z M 63.144531 108.402344 ;" +
            "Yellow|M 78.898438 110.269531 C 75.027344 115.210938 75.292969 122.152344 79.433594 126.15625 C 83.4375 130.296875 84.773438 130.296875 88.777344 126.15625 C 95.050781 120.015625 91.980469 106.800781 84.238281 106.800781 C 82.769531 106.800781 80.367188 108.402344 78.898438 110.269531 Z M 86.773438 112.273438 C 86.773438 113.875 82.902344 117.347656 81.167969 117.480469 C 79.566406 117.480469 79.964844 114.140625 81.703125 112.40625 C 83.703125 110.40625 86.773438 110.269531 86.773438 112.273438 Z M 86.242188 123.886719 C 84.503906 125.625 83.839844 125.757812 82.503906 124.421875 C 81.035156 122.953125 81.433594 122.152344 84.105469 120.148438 C 87.042969 117.878906 87.441406 117.878906 87.84375 119.75 C 88.109375 120.816406 87.441406 122.820312 86.242188 123.886719 Z M 86.242188 123.886719 ;" +
            "Yellow|M 98.523438 108.9375 C 94.25 111.207031 93.449219 114.140625 97.453125 113.074219 C 99.859375 112.40625 100.125 113.074219 100.125 118.949219 C 100.125 124.554688 99.722656 125.488281 97.453125 125.488281 C 95.988281 125.488281 94.785156 126.023438 94.785156 126.824219 C 94.785156 127.492188 98.121094 128.160156 102.128906 128.160156 C 109.335938 128.160156 112.007812 126.425781 106.800781 125.089844 C 104.53125 124.554688 104.128906 123.21875 104.128906 115.609375 C 104.128906 110.804688 103.730469 106.800781 103.195312 106.800781 C 102.527344 106.933594 100.527344 107.867188 98.523438 108.9375 Z M 98.523438 108.9375 ;" +
            "Yellow|M 114.277344 109.335938 C 110.671875 113.339844 111.070312 124.023438 114.945312 127.09375 C 120.683594 131.898438 126.824219 127.226562 126.824219 117.878906 C 126.824219 108.535156 119.484375 103.597656 114.277344 109.335938 Z M 122.152344 112.140625 C 122.953125 113.476562 116.546875 118.28125 115.34375 117.078125 C 114.945312 116.679688 115.210938 115.078125 116.144531 113.609375 C 117.613281 110.671875 120.816406 109.871094 122.152344 112.140625 Z M 122.820312 120.816406 C 122.820312 124.554688 119.484375 126.691406 117.214844 124.421875 C 116.144531 123.355469 116.546875 122.152344 118.949219 120.285156 C 122.953125 116.8125 122.820312 116.8125 122.820312 120.816406 Z M 122.820312 120.816406 ;" +
            "Yellow|M 133.097656 108.9375 C 129.09375 111.070312 129.496094 114.140625 133.632812 112.808594 C 135.902344 112.007812 136.171875 112.675781 136.171875 118.148438 C 136.171875 123.21875 135.636719 124.554688 133.5 125.089844 C 128.558594 126.425781 130.828125 128.160156 137.503906 128.160156 C 141.242188 128.160156 144.179688 127.492188 144.179688 126.824219 C 144.179688 126.023438 143.246094 125.488281 142.175781 125.488281 C 140.574219 125.488281 140.175781 123.621094 140.175781 116.144531 C 140.175781 106 139.640625 105.464844 133.097656 108.9375 Z M 133.097656 108.9375 ;", 
            typeof(Playground), new Info()) { }
    }
}
