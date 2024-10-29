# Project 2 Report

Read the [project 2 specification](https://github.com/feit-comp30019/project-2-specification) for details on what needs to be covered here. You may modify this template as you see fit, but please keep the same general structure and headings.

Remember that you must also continue to maintain the Game Design Document (GDD) in the `GDD.md` file (as discussed in the specification). We've provided a placeholder for it [here](GDD.md).

---

## Table of Contents

- [Evaluation Plan](#evaluation-plan)
- [Evaluation Report](#evaluation-report)
- [Shaders and Special Effects](#shaders-and-special-effects)
- [Summary of Contributions](#summary-of-contributions)
- [References and External Resources](#references-and-external-resources)

---

## Evaluation Plan

### Observational Technique - Cooperative Evaluation

- Analyst and participant evaluate the game together.
- Identified problems and potential solutions are discussed together with participants.
- Questions to ask participants:
   - How did the atmosphere of the game make you feel throughout your gameplay experience?
   - What aspect of the game stood out to you the most?
   - Was there any moment where you felt confused or uncertain about what to do next?
   - How did the eerie elements (SFX) affect your exploration of the game environment?
   - How are the controls?
   - What do you like most about our game?
   - What do you think could be improved?
   - What do you think of the difficulty of the game?
   - What do you think the game is lacking to achieve a more eerie effect?
   - What part of the game do you think is the most engaging? What part feels boring?
   - Were there any moments where the atmosphere didn’t feel eerie/unsettling? What contributed to that?
   - What are your thoughts when balancing your sanity bar and crafting light sources in the game? Did you face any challenges?

### Protocol for Game Testing - Cooperative Evaluation 

#### Overview

This protocol outlines a process for conducting a cooperative evaluation of test players engaging with a game, focusing on collaborative assessment and feedback.

#### Objectives

- Observe player interactions.
- Identify issues and gather feedback.
- Discuss potential solutions together.

#### Preparation

1. **Select Participants:** 
   - Recruit at least 5 players representing your target audience.

2. **Set Up Environment:** 
   - Ensure a comfortable, distraction-free space with all necessary equipment.

3. **Materials:** 
   - Prepare observation checklists and recording tools (with consent).

#### Protocol Steps

1. **Introduction (5 min):**
   - Introduce participants and explain the purpose of the evaluation.

2. **Gameplay Session (15 min):**
   - Allow participants to play while analysts observe and take notes.

3. **Cooperative Discussion (10 min):**
   - Gather for a discussion, encouraging participants to share their experiences.
   - Analysts facilitate conversation about identified issues and potential solutions.

4. **Wrap-Up (5 min):**
   - Summarise findings and thank participants for their input.

#### Post-Session

1. **Analyse Notes:**
   - Review notes and recordings, compiling a report on key findings.

2. **Prioritise Issues:**
   - Rank issues based on how often they occur and their impact, discussing with the development team.

3. **Follow-Up:**
   - Share findings with everyone involved and plan future evaluations if necessary.

---

### Query Technique - Questionnaire

- Participants will play the game on WebGL and fill out a survey form.
- The survey form will include sections to rate various aspects of the gameplay and give text feedback.
-  Things we want feedback/ratings on:
   - How does the player feel whilst playing the game?
   - What do they think about the sanity system (does it stress them out)?
   - Does the player feel incentivized to craft light sources?
- Link to questionnaire: https://forms.gle/CN99nJRuxXeCCNT59

### Query Technique Protocol for Game Testing

#### Overview

This protocol outlines the process for using questionnaires to gather feedback from participants after playing a game on WebGL. The goal is to assess various aspects of the gameplay and collect both quantitative and qualitative feedback.

#### Objectives

- Gather participant ratings on different gameplay aspects.
- Collect textual feedback for each gameplay aspect and the overall game experience.

#### Preparation

1. **Select Participants:**
   - Recruit at least 5 players representing your target audience.

2. **Set Up Environment:**
   - Ensure a comfortable, distraction-free space with access to the game on WebGL.

3. **Materials:**
   - Prepare a questionnaire that includes:
     - Rating scales for different gameplay aspects.
     - Open-ended text fields for feedback.

#### Questionnaire Structure

1. **Gameplay Rating Section:**
   - Include a rating scale (e.g., 1 to 5) for each aspect, such as:
     - Graphics
     - Controls
     - Difficulty
     - Enjoyment

2. **Feedback Section:**
   - Provide text fields for participants to offer detailed feedback:
     - Comments on each gameplay aspect.
     - Overall feedback on the game.

#### Protocol Steps

1. **Introduction (5 min):**
   - Introduce participants to the study and explain the purpose of the questionnaire.

2. **Gameplay Session (15 min):**
   - Allow participants to play the game on WebGL for the allocated time.

3. **Questionnaire Completion (10 min):**
   - Direct participants to fill out the survey form after gameplay.
   - Encourage them to provide honest feedback and explanations for their ratings.

4. **Wrap-Up (5 min):**
   - Thank participants for their time and contributions.
   - Remind them of the importance of their feedback for game development.

#### Post-Session

1. **Analyse Responses:**
   - Review the completed questionnaires, compiling quantitative ratings and qualitative feedback.

2. **Identify Key Themes:**
   - Look for common trends in ratings and comments to highlight areas for improvement.

3. **Follow-Up:**
   - Share findings with the development team and plan for any necessary adjustments based on participant feedback.

---

### Participants

- **Target Audience**: Teenagers and young adults aged 15 - 28.
- **Recruitment Methods**:
  1. Ask university friends and family to volunteer.
  2. Post on social media/online platforms such as Reddit.

---

### Data Collection

- **Quantitative Data**:
  - Gameplay rating: Game mechanic / Graphic / Theme.
  - Satisfaction.
  
- **Qualitative Data**:
  - Feedback: What they like/dislike, and what could be improved.

- **Tools**: Use a questionnaire (survey) for both quantitative and qualitative data.

---

### Data Analysis

#### For Observational Technique:
- **Affinity Diagram**: Analyse main findings by grouping similar insights.

#### For Questionnaire:
- **Summary Statistics**: Analyse summary statistics of quantitative data (i.e. mean, median, standard deviation).
- **Visual Graphs**: Produce graphs of the scores for each aspect of the game to identify areas that need improvement.

---

### Timeline
![Evaluation-Timeline](./ReportImages/evaluation_plan_timeline.png)

---

### Responsibilities

Who is responsible for each task? How will you ensure that everyone contributes equally?

| Team member | Responsibility            |
|-------------|----------------------------|
| Tan         | Observational Technique    |
| Cala        | Query Technique            |
| Don Lam     | Observational Technique    |
| Giang       | Query Technique            |

---

## Evaluation Report

TODO (due milestone 3) - see specification for details

---

## Shaders and Special Effects

### Shader #1 - Blueprint Shader

**Blueprint Shader File:** [link](./Assets/_Shaders/BlueprintShader.shader) 

**Description:**  
The Blueprint Shader creates a holographic effect on items being placed in the game world. Its color is adjustable via uniform variables in a C# script. When an item is blocked by obstacles, it appears red; otherwise, it appears blue to indicate placement readiness.

<div align="center">
  <img src="./ReportImages/ShadersFX/blueprint_shader_gif.gif" alt="Blueprint Shader Demo" />
  <p><strong>Blueprint Shader Demo</strong></p>
</div>

**Rationale:**  
Crafting is central to gameplay, so polished feedback during item placement improves the player experience. This shader visually indicates placement states, reducing confusion and enhancing interaction with crafting mechanics.

**Theory:**  
The vertex shader is untouched given that we do not wish to displace vertices of the input object’s mesh for a blueprint / hologram effect. Instead, to emulate a hologram effect, the fragment shader renders bars running across the object using the UV coordinates and the sine wave function to set output rgb to 0 at set intervals. The animation of the bars moving is done by translating the sine wave function with respect to game time.

**Material Parameters:**
- [Blueprint Material Link](./Assets/_Shaders/BlueprintShader_Material.mat)

<br/>

<div align="center">
  <img src="./ReportImages/ShadersFX/_HoloIntensity.gif" alt="_HoloIntensity Parameter Demo" />
  <p style="margin-top: 7px;"><strong>_HoloIntensity: Controls bar quantity; higher values increase bar density.</strong></p>
</div>

<br/>

<div align="center">
  <img src="./ReportImages/ShadersFX/_AnimIntensity.gif" alt="_AnimIntensity Parameter Demo" />
  <p style="margin-top: 7px;"><strong>_AnimIntensity: Sets bar movement speed; higher values increase speed.</strong></p>
</div>

<br/>

<div align="center">
  <img src="./ReportImages/ShadersFX/_Rotator.gif" alt="_Rotator Parameter Demo" />
  <p style="margin-top: 7px;"><strong>Determines bar orientation; 0 is vertical, 1 is horizontal, interpolated between.</strong></p>
</div>

**C# Script for Color Parameters:**
- [C# Script Colour Modification Link](./Assets/_Scripts/Crafting/PlaceItem.cs)

- The parameters being set in this C# script are _Color1 and _Color2. The colour of the object is interpolated between _Color1 and _Color2 to form a gradient. This makes the object’s colour more dynamic and complex.

<br/>

<div align="center">
  <pre style="width: 80%; max-width: 600px; text-align: left; padding: 10px; border-radius: 5px;">
   private void SetPlaceable()
   {
      ... Some other code here
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
               material.SetColor("_Color1", new Color(0.0f, 0.0f, 0.55f));
               material.SetColor("_Color2", new Color(0.68f, 0.85f, 0.9f));
            }
        }
      ... Some other code here
   }
  </pre>

  <p><strong>The colour scheme of the object is set to blue when the item can be placed at the current location.</strong></p>
</div>

<br/>

<div align="center">
  <pre style="width: 80%; max-width: 600px; text-align: left; padding: 10px; border-radius: 5px;">
   private void SetUnplaceable()
   {
      ... Some other code here
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
               material.SetColor("_Color1", new Color(0.55f, 0.0f, 0.0f));
               material.SetColor("_Color2", new Color(1.0f, 0.6f, 0.6f));
            }
        }
      ... Some other code here
   }
  </pre>

  <p><strong>The colour scheme of the object is set to red when the item cannot be placed at the current location.</strong></p>
</div>

---

### Shader #2 - Ghost Shader

**Ghost Shader File**: [link](./Assets/_Shaders/GhostShader.shader)

**Description:**  
The Ghost Shader is used to create a material for ghosts, giving them a slightly distorted and semi-transparent look. Transparency can be adjusted through a uniform variable in a C# script. This allows for a smooth “fading in” effect, enhancing the ghost's appearance as it materializes in the game.

<div align="center">
  <img src="./ReportImages/ShadersFX/ghost_shader_gif.gif" alt="Ghost Shader Demo" />
  <p><strong>Ghost Shader Demo</strong></p>
</div>

**Rationale:**  
The Ghost Shader is essential for our game, as ghosts are a key feature that adds to its eerie atmosphere. The shader creates a distorted, transparent appearance that enhances the creepy effect of the ghosts. It is difficult to do vertex distortion purely through C# code, which is why a shader is used. Additionally, the ability to adjust parameters for a ghost fade-in effect adds polish to the game, ensuring a smooth transition when the ghost appears.

**Theory:**  
To create the distortion effect, the vertex shader displaces the input vertex by a sine wave function that is also translated with respect to the game time. This creates a moving wave animation on the ghost’s body. In addition, to give the ghost a more “ghostly” appearance, the fragment shader modifies the output alpha channel and RGB values to render the ghost more transparent and dimmed.

**Material Parameters:**  
- [Ghost Material Link](./Assets/_Shaders/BlueprintShader_Material.mat)

<br/>

<div align="center">
  <img src="./ReportImages/ShadersFX/3ghost.gif" alt="_MainTex Parameter Demo" />
  <p style="margin-top: 7px;"><strong>_MainTex allows the ghost to have different textures applied. Above are the ghosts with 3 different textures.</strong></p>
</div>

<br/>

<div align="center">
  <img src="./ReportImages/ShadersFX/_Transparency.gif" alt="_Transparency Parameter Demo" />
  <p style="margin-top: 7px;"><strong>_Transparency adjusts the see-through effect.</strong></p>
</div>

<br/>

<div align="center">
  <img src="./ReportImages/ShadersFX/_Darkness.gif" alt="_Darkness Parameter Demo" />
  <p style="margin-top: 7px;"><strong>_Darkness adjusts the dimness of the output RGB channels</strong></p>
</div>

<br/>

<div align="center">
  <img src="./ReportImages/ShadersFX/_AuraColor.gif" alt="_AuraColor Parameter Demo" />
  <p style="margin-top: 7px;"><strong>_AuraColor determines the colour of the ghost.</strong></p>
</div>

<br/>

<div align="center">
  <img src="./ReportImages/ShadersFX/_Distortion.gif" alt="_Distortion Parameter Demo" />
  <p style="margin-top: 7px;"><strong>_DistortionAmount adjusts distortion intensity.</strong></p>
</div>

<br/>

<div align="center">
  <img src="./ReportImages/ShadersFX/_TimeScale.gif" alt="_TimeScale Parameter Demo" />
  <p style="margin-top: 7px;"><strong>_TimeScale adjusts wave animation speed.</strong></p>
</div>

**C# Script for Fading Effect:**  
- [C# Script Fading Effect Link](./Assets/_Scripts/WorldObjects/GhostController.cs)

<br/>

<div align="center">
  <pre style="width: 90%; max-width: 900px; text-align: left; padding: 10px; border-radius: 5px;">
   private IEnumerator FadeIn()
   {
      float elapsedTime = 0;
      while (elapsedTime < fadeDuration)
      {
         // Increment elapsed time
         elapsedTime += Time.deltaTime;

         // Calculate the new transparency and aura color values
         float t = elapsedTime / fadeDuration;

         // Restore transparency and aura color over time
         float newTransparency = Mathf.Lerp(0, originalTransparency, t); // From 0 to original transparency
         originalAuraColor.a = Mathf.Lerp(0, originalAuraColor.a, t); // From 0 to original aura alpha

         // Apply the new values to the material
         ghostRenderer.material.SetFloat("_Transparency", newTransparency);
         ghostRenderer.material.SetColor("_AuraColor", originalAuraColor);

         yield return null; // Wait until the next frame
      }
      // Ensure the final transparency and aura color are set to their original values
      ghostRenderer.material.SetFloat("_Transparency", originalTransparency);
      originalAuraColor.a = 1; // Ensure aura color is fully opaque
      ghostRenderer.material.SetColor("_AuraColor", originalAuraColor);
   }
  </pre>

  <p><strong>_Transparency and _AuraColor are interpolated with respect to animation time.</strong></p>
</div>

---

### Fog Particle System

**Fog Particle System File:** [link](./Assets/EnvironmentAsset/Fog/FogParticleSystem.prefab)

**Description:**  
The fog particle system generates volumetric fog that enriches the game environment. This fog enhances the overall aesthetics and contributes to the game's eerie atmosphere.

**Rationale:**  
The fog particle system is essential for our game, as it deepens the eerie ambiance and unifies the visual elements. By adding fog, we create a more immersive experience, making the environment feel more atmospheric and visually appealing.

<div align="center">
  <img src="./ReportImages/ShadersFX/fog_particle_gif.gif" alt="Fog Particle System Demo" />
  <p><strong>Fog vs No Fog Demo</strong></p>
</div>

**Randomized Attributes for Natural Fog Effects:**  

- **Start Speed:**  
   <br/>
   <div align="center">
      <img src="./ReportImages/ShadersFX/randomise_start_speed.png" alt="Start Speed Parameter" />
      <p style="margin-top: 7px;"><strong>Speed randomised between 0 and 2 for natural dispersion.</strong></p>
   </div>  
  
  **Description:** Start speed defines the initial velocity of each particle, influencing how fast particles move upon spawning. By randomizing this value, each particle has a slight variation in speed, leading to a more natural, dispersed fog effect.

  **Rationale:** Fog in nature doesn’t move at a uniform speed; pockets of mist vary in movement. The randomness in start speed gives the fog a more organic, drifting appearance, enhancing realism.

- **Start Rotation:**  
   <br/>
   <div align="center">
      <img src="./ReportImages/ShadersFX/randomise_start_rotation.png" style="width: 500px; height: auto;" alt="Start Speed Parameter" />
      <p style="margin-top: 7px;"><strong>Rotation follows a curve, simulating swirling fog.</strong></p>
   </div>  
    
  **Description:** Start rotation defines the initial orientation of each particle. The rotation curve ensures particles have gradual orientation shifts. Starting at a low rotation and increasing along a curve means particles will gradually adjust their angles.

  **Rationale:** This curve simulates the swirling and slight rotation typical of fog or mist, making it appear as though the fog is naturally turning and blending within itself. The curve shape makes the rotation predictable but still dynamic, aligning with the slow, rolling nature of fog.

- **Randomise Direction:**  
   <br/>
   <div align="center">
      <img src="./ReportImages/ShadersFX/randomise_direction.png" alt="Start Speed Parameter" />
      <p style="margin-top: 7px;"><strong>Particles emit in random directions for realistic diffusion. </strong></p>
   </div>   
   
  **Description:** When this option is enabled, particles are emitted in random directions rather than following a strict path.  
  
  **Rationale:** Fog generally disperses in all directions, so randomizing the emission direction allows particles to spread unpredictably, achieving a more diffused, realistic fog volume. This also prevents unnatural movement patterns and avoids the "layered" look common with directional fog particles.


---

## Summary of Contributions

TODO (due milestone 3) - see specification for details

---

## References and External Resources

TODO (to be continuously updated) - see specification for details
