using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;

    public struct AgentPlayerCheckJob : IJobParallelFor
    {
        [ReadOnly] public float3 playerPos;
        [ReadOnly] public float detectionRadius;
        [ReadOnly] public NativeArray<float3> AgentPositions;
        
        public NativeArray<float3> CashedPlayerPositions;

        [WriteOnly] public NativeArray<float3> Results;
        
        public void Execute(int index)
        {
            float3 agentPos = AgentPositions[index];
            float distanceSq = math.distance(agentPos, playerPos);

            if (distanceSq > detectionRadius * detectionRadius)
            {
                CashedPlayerPositions[index] = playerPos;
                Results[index] = playerPos;
            }
            else
            {
                Results[index] = CashedPlayerPositions[index];
            }
        }
    }
