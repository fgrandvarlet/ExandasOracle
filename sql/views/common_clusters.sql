
CREATE VIEW common_clusters AS
SELECT
    cluster_name
FROM src_clusters
JOIN tgt_clusters USING (cluster_name)
;
