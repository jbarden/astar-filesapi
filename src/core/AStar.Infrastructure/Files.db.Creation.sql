BEGIN TRANSACTION;
DROP TABLE IF EXISTS "TagsToIgnore";
CREATE TABLE IF NOT EXISTS "TagsToIgnore" (
	"Value"	TEXT NOT NULL COLLATE NOCASE,
	CONSTRAINT "PK_TagsToIgnore" PRIMARY KEY("Value")
);
DROP TABLE IF EXISTS "TagsToIgnoreCompletely";
CREATE TABLE IF NOT EXISTS "TagsToIgnoreCompletely" (
	"Value"	TEXT NOT NULL COLLATE NOCASE,
	CONSTRAINT "PK_TagsToIgnoreCompletely" PRIMARY KEY("Value")
);
DROP TABLE IF EXISTS "Files";
CREATE TABLE IF NOT EXISTS "Files" (
	"FileName"	TEXT NOT NULL COLLATE NOCASE,
	"DirectoryName"	TEXT NOT NULL COLLATE NOCASE,
	"DetailsLastUpdated"	TEXT,
	"LastViewed"	TEXT,
	"Height"	INTEGER NOT NULL DEFAULT 0,
	"Width"	INTEGER NOT NULL DEFAULT 0,
	"FileSize"	INTEGER NOT NULL DEFAULT 0,
	"SoftDeleted"	INTEGER NOT NULL DEFAULT 0,
	"SoftDeletePending"	INTEGER NOT NULL DEFAULT 0,
	"NeedsToMove"	INTEGER NOT NULL DEFAULT 0,
	"HardDeletePending"	INTEGER NOT NULL DEFAULT 0,
	CONSTRAINT "PK_Files" PRIMARY KEY("FileName","DirectoryName")
);
DROP INDEX IF EXISTS "ixDirectoryAscFileDesc";
CREATE INDEX IF NOT EXISTS "ixDirectoryAscFileDesc" ON "Files" (
	"DirectoryName"	ASC,
	"FileName"	DESC
);
DROP INDEX IF EXISTS "ixDirectoryDescFileDesc";
CREATE INDEX IF NOT EXISTS "ixDirectoryDescFileDesc" ON "Files" (
	"DirectoryName"	DESC,
	"FileName"	DESC
);
DROP INDEX IF EXISTS "ixFileSizeAsc";
CREATE INDEX IF NOT EXISTS "ixFileSizeAsc" ON "Files" (
	"FileSize"	ASC
);
DROP INDEX IF EXISTS "ixFileSizeDesc";
CREATE INDEX IF NOT EXISTS "ixFileSizeDesc" ON "Files" (
	"FileSize"	DESC
);
DROP INDEX IF EXISTS "ixSoftDeletedAsc";
CREATE INDEX IF NOT EXISTS "ixSoftDeletedAsc" ON "Files" (
	"SoftDeleted"	ASC
);
DROP INDEX IF EXISTS "ixSoftDeletedDesc";
CREATE INDEX IF NOT EXISTS "ixSoftDeletedDesc" ON "Files" (
	"SoftDeleted"	DESC
);
DROP INDEX IF EXISTS "ixSoftDeletePendingAsc";
CREATE INDEX IF NOT EXISTS "ixSoftDeletePendingAsc" ON "Files" (
	"SoftDeletePending"	ASC
);
DROP INDEX IF EXISTS "ixSoftDeletePendingDesc";
CREATE INDEX IF NOT EXISTS "ixSoftDeletePendingDesc" ON "Files" (
	"SoftDeletePending"	DESC
);
DROP INDEX IF EXISTS "ixNeedsToMoveAsc";
CREATE INDEX IF NOT EXISTS "ixNeedsToMoveAsc" ON "Files" (
	"NeedsToMove"	ASC
);
DROP INDEX IF EXISTS "ixNeedsToMoveDesc";
CREATE INDEX IF NOT EXISTS "ixNeedsToMoveDesc" ON "Files" (
	"NeedsToMove"	DESC
);
DROP INDEX IF EXISTS "ixHardDeletePendingAsc";
CREATE INDEX IF NOT EXISTS "ixHardDeletePendingAsc" ON "Files" (
	"HardDeletePending"	ASC
);
DROP INDEX IF EXISTS "ixHardDeletePendingDesc";
CREATE INDEX IF NOT EXISTS "ixHardDeletePendingDesc" ON "Files" (
	"HardDeletePending"	DESC
);
DROP INDEX IF EXISTS "ixLastViewedAsc";
CREATE INDEX IF NOT EXISTS "ixLastViewedAsc" ON "Files" (
	"LastViewed"	ASC
);
DROP INDEX IF EXISTS "ixLastViewedDesc";
CREATE INDEX IF NOT EXISTS "ixLastViewedDesc" ON "Files" (
	"LastViewed"	DESC
);
COMMIT;
