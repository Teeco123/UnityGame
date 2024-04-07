INCLUDE globals.ink

#speaker:ziutek
Siema czego chcesz?
-> wybory


=== wybory ===
+   [Bywaj]
        Dobrze bywaj w takim razie
        -> END
        
+   {not piwko}[Kupie piwo]
        Jakie chcialbys te piwko?
        -> kupnoPiwa



=== kupnoPiwa ===
+[Tyskie]
    Trzymaj tyskie
    ~ wybranePiwko = "Tyskie"
    -> wybory
+[Harnas]
    Trzymaj harnasia
    ~ wybranePiwko = "Harnas"
    -> wybory
+[Piwko Tesco]
    Sigma masz piwko tesco
    ~ wybranePiwko = "Tesco"
    -> wybory

