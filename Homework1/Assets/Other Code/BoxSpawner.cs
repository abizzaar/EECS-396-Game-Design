/*
 * Copyright (c) 2016 Ian Horswill
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
 * associated documentation files (the "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial
 * portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN
 * NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets
{
    /// <summary>
    /// Creates falling boxes at the top of the screen.
    /// </summary>
    public class BoxSpawner : MonoBehaviour
    {
        [Header("Object drop from the top of the screen")]
        public Transform Prefab;

        [Header("How often to drop")]
        public float InitialSpawnDelay = 2;
        public float SpawnInterval = 5;
        public float SpawnAcceleration = 0.1f;
        public float MinSpawnInterval = 2;
        [Header("Where to drop")]
        public float SpawnHeight = 50;
        public float MinX = -50;
        public float MaxX = 50;
        public float Spacing = 0.5f;

        /// <summary>
        /// Next location to drop from
        /// </summary>
        private float nextX;

        /// <summary>
        /// Just spawn a coroutine to instantiate boxes every SpawnInterval seconds.
        /// </summary>
        [UsedImplicitly]
        internal System.Collections.IEnumerator Start () {
            nextX = MinX;

            yield return new WaitForSeconds(InitialSpawnDelay);

            while (true)
            {
                // Make the box
                Instantiate(Prefab, new Vector3(nextX, SpawnHeight, 0), Quaternion.identity);

                // Set up for the next box drop.
                nextX += Spacing;
                if (nextX > MaxX)
                    nextX = MinX;

                // Schedule the next box drop
                SpawnInterval = Math.Max(MinSpawnInterval, SpawnInterval - SpawnAcceleration);
                yield return new WaitForSeconds(SpawnInterval);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}
