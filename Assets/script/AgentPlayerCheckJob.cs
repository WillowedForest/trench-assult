using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

public struct AgentPlayerCheckJob : IJobParallelFor
    {
        [ReadOnly] public float detectionRadius;
        [ReadOnly] public NativeArray<float3> AgentPositions;
        
        [ReadOnly] public float3 CashedPlayerPosition;

        [WriteOnly] public NativeArray<bool> Results;
        
        public void Execute(int index)
        {
            float3 zero = float3.zero;
            
            if (CashedPlayerPosition.x == zero.x || CashedPlayerPosition.y == zero.y ||
                CashedPlayerPosition.z == zero.z)
            {
                Results[index] = true;
                return;
            }
            
            float3 differance = CashedPlayerPosition - AgentPositions[index];

            float distance = Mathf.Sqrt(
                Mathf.Pow(differance.x, 2f) +
                Mathf.Pow(differance.y, 2f) +
                Mathf.Pow(differance.z, 2f));
            


            if (distance <= detectionRadius)
            {
                Results[index] = true;
            }
            else
            {
                
                Results[index] = false;
            }
            
        }
    }
