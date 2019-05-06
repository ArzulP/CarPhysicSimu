# CarPhysicSimu

## Contrôles

A noter que les deux moyens de contrôle peuvent être utilisés en même temps.

### Pour clavier

Avancer : flèche avant OU touche 'W'
Freiner : flèche arrière OU touche 'S'
Tourner : flèche gauche/droite OU touche 'a'/'d'
Marche arrière : touche espace ou 'x'
Reset position : touche 'r'

### Pour manette xbox360

Avancer : gâchette droite
Freiner : bouton 'A'
Tourner : stick gauche
Marche arrière : bouton 'B'
Reset position : bouton 'Y'

## Description et fonctionnalités

CarPhysicSimu a pour objectif de simuler le comportement d'un véhicule. La simulation propose actuellement une représentation simple de la physique réelle dans un environnement de test.
Sont gérés par la simulation:
* Accélération progressive du véhicule
* Freinage du véhicule
* Marche arrière du véhicule
* Gestion de la répartition du poids
*  Déplacement des roues en fonction de la répartition
*  Rotation du véhicule en fonction de la répartition
*  Impacte global sur le comportement du véhicule selon la répartition
* Système de dérapage
*  Dérapage lorsque trop grande force appliquée
*  Différent type de terrain aux frottements différents
*   Routes, deux petites routes rectilignes
*   Givre, situé sur le côté de la route
*   Terre, représenté par un cadrillage orange/vert
*   Roche, situé au bout de la piste
* Gestion des particules
*  Activation des particules de fumée lors de freinage
* Repositionnement du véhicule par pression de touche si besoin
* Caméra simple suivant le véhicule

## Bug connus

En marche arrière l'effet de fumée s'active de temps en temps. Cela intervient lorsque le véhicule atteint la vitesse maximum de marche arrière. La structure actuelle de gestion de particule ne permet pas de remédier à ce problème.
